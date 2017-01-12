using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Windows;
using Novacode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileManager
{
    public partial class MainViewModel
    {

        private void SubmitAction(object obj)
        {
            var ck = VerifyFileContents();
            if (ck == false)
            {
                return;
            }
            if (TempFile.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "请选择模板文件");
                return;
            }
            if (!File.Exists(TempFile))
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "模板文件不存在");
                return;
            }
            if (TargetDir.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "请选择目标目录");
                return;
            }

            Delete();
            AddOrUpdate();

            MessageWindow.ShowMsg(MessageType.Info, "提示", "修改成功");

            if (OpenAfterSubmit)
            {
                Process.Start("Explorer.exe", TargetDir);
            }
        }

        private void Delete()
        {
            DirectoryInfo dirTarget = new DirectoryInfo(TargetDir);
            if (!dirTarget.Exists)
            {
                dirTarget.Create();
                return;
            }
            //1、删除列表中不存在的文件
            //1.1清空
            if (FileContents.IsEmpty())
            {
                //将要清空目标目录
                dirTarget.Delete(true);
                dirTarget.Create();
                return;
            }
            //1.2删除部分多余文件
            var lstNames = FileContents.Select(fc => fc.filecode + fc.filename);
            var filesToDel = dirTarget.GetFiles().Where(f => !lstNames.Contains(f.Name.Substring(0, f.Name.IndexOf('.'))));
            if (filesToDel.IsNotEmpty())
            {
                filesToDel.ToList().ForEach(f => f.Delete());
            }
        }

        private void AddOrUpdate()
        {
            if (FileContents.IsEmpty())
            {
                return;
            }
            //2、新增或修改文件
            foreach (var fileCnt in FileContents)
            {
                var destFileName = TargetDir.TrimEnd('/', '\\') + "/" + (fileCnt.filecode + fileCnt.filename) + ".docx";
                //新增
                if (!File.Exists(destFileName))
                {
                    File.Copy(TempFile, destFileName);
                }
                //修改
                //2.1页眉页脚
                using (DocX doc = DocX.Load(destFileName))
                {
                    UpdateDoc(doc, fileCnt);
                }
            }
        }

        private bool VerifyFileContents()
        {
            if (FileContents.IsEmpty())
            {
                return true;
            }
            var gpCnt = FileContents.GroupBy(fc => fc.filecode + fc.filename).Count();
            if (gpCnt != FileContents.Count())
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "列表中存在重复的【文件编号+文件名称】组合");
                return false;
            }
            return true;
        }

        private void UpdateDoc(DocX doc, FileContent fileCnt)
        {
            //1、页眉
            //logo
            if (fileCnt.logo.IsNotEmpty() && File.Exists(fileCnt.logo))
            {
                Novacode.Image img = doc.AddImage(fileCnt.logo);
                AddLogoToHeader(doc.Headers.odd, img);
                AddLogoToHeader(doc.Headers.first, img, 100, 100);
            }

            doc.ReplaceText("A2", fileCnt.filename);
            doc.ReplaceText("A3", fileCnt.filecode);
            doc.ReplaceText("A4", fileCnt.versioncode);
            doc.ReplaceText("A5", fileCnt.efdate);

            //2、表格
            doc.ReplaceText("A6", fileCnt.draftdept);
            doc.ReplaceText("A7", fileCnt.checkdept1);
            doc.ReplaceText("A8", fileCnt.checkdept2);
            doc.ReplaceText("A9", fileCnt.checkdept3);
            doc.ReplaceText("A10", fileCnt.approver);

            doc.ReplaceText("A11", "1");
            doc.ReplaceText("A12", fileCnt.remark);
            doc.ReplaceText("A13", fileCnt.writer);

            doc.Save();
        }

        private void AddLogoToHeader(Header header, Image img, int height = 80, int width = 80)
        {
            var p = header.Paragraphs[0];
            if (p.Pictures.IsNotEmpty())
            {
                //TODO
                //避免插入多个pic，这里应该先删除pic然后再插入，待修改
                return;
            }
            p.Alignment = Alignment.center;

            var pic = img.CreatePicture(height, width);
            p.InsertPicture(pic, 0);

            //header.RemoveParagraphAt(1);

        }
    }
}
