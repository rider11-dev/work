using FileManager;
using Microsoft.Win32;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using MyNet.Components.Office;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace FileManager
{
    public class MainViewModel:BaseModel
    {
        const string Key_MngFile = "last_mng_file";
        const string Key_TargetDir = "last_target_dir";
        const string Key_DelNotList = "del_not_list";
        const string Key_OpenAfterSubmit = "open_after_submit";
        public MainViewModel()
        {
            MngFile = AppSettingUtils.Get(Key_MngFile);
            TargetDir= AppSettingUtils.Get(Key_TargetDir);
            DelFileNotList = Convert.ToBoolean(AppSettingUtils.Get(Key_DelNotList));
            OpenAfterSubmit = Convert.ToBoolean(AppSettingUtils.Get(Key_OpenAfterSubmit));
        }

        private IEnumerable<FileContent> _fileCnts;
        public IEnumerable<FileContent> FileContents { get { return _fileCnts; }
            set {
                if(_fileCnts!=value)
                {
                    _fileCnts = value;
                    base.RaisePropertyChanged("FileContents");
                }
            } }
        private string _mngFile;
        public string MngFile
        {
            get { return _mngFile; }
            set
            {
                if(_mngFile!=value)
                {
                    _mngFile = value;
                    base.RaisePropertyChanged("MngFile");
                }
            }
        }
        private bool _delFileNotList;
        public bool DelFileNotList
        {
            get { return _delFileNotList; }
            set
            {
                if (_delFileNotList != value)
                {
                    _delFileNotList = value;
                    base.RaisePropertyChanged("DelFileNotList");
                    AppSettingUtils.Update(Key_DelNotList, _delFileNotList.ToString());
                }
            }
        }
        private bool _openAfterSubmit;
        public bool OpenAfterSubmit
        {
            get { return _openAfterSubmit; }
            set
            {
                if (_openAfterSubmit != value)
                {
                    _openAfterSubmit = value;
                    base.RaisePropertyChanged("OpenAfterSubmit");
                    AppSettingUtils.Update(Key_OpenAfterSubmit, _openAfterSubmit.ToString());
                }
            }
        }
        private string _targetDir;
        public string TargetDir
        {
            get { return _targetDir; }
            set
            {
                if (_targetDir != value)
                {
                    _targetDir = value;
                    base.RaisePropertyChanged("TargetDir");
                }
            }
        }
        ICommand _selectMngFileCmd;
        public ICommand SelectMngFileCmd
        {
            get
            {
                if(_selectMngFileCmd==null)
                {
                    _selectMngFileCmd = new DelegateCommand(SelectMngFileAction);
                }
                return _selectMngFileCmd;
            }
        }

        private void SelectMngFileAction(object obj)
        {
            Microsoft.Win32.OpenFileDialog openFileDia = new Microsoft.Win32.OpenFileDialog();
            openFileDia.Filter = "Excel Files|*.xls;*.xlsx";
            var rst = openFileDia.ShowDialog();
            if(rst.HasValue && rst == true)
            {
                MngFile = openFileDia.FileName;
                AppSettingUtils.Update(Key_MngFile, MngFile);
            }
        }

        ICommand _parseMngFileCmd;
        public ICommand ParseMngFileCmd
        {
            get
            {
                if (_parseMngFileCmd == null)
                {
                    _parseMngFileCmd = new DelegateCommand(ParseMngFileAction);
                }
                return _parseMngFileCmd;
            }
        }

        private void ParseMngFileAction(object obj)
        {
            if(MngFile.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "请选择管理文件");
                return;
            }
            if(!File.Exists(MngFile))
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "管理文件不存在");
                return;
            }
            IEnumerable<FileContent> fileCnts = ReadMngFile(MngFile);
            FileContents = fileCnts;
        }

        private IEnumerable<FileContent> ReadMngFile(string filename)
        {
            var colNames = new string[] { "filename", "filecode", "versioncode", "efdate", "draftdept", "checkdept1", "checkdept2", "checkdept3", "checker", "logo" };
            var cols = new Dictionary<int, string>();
            for(int idx=0;idx<10;idx++)
            {
                cols.Add(idx, colNames[idx]);
            }
            var datas= ExcelUtils.ReadFile(filename:filename,cols: cols);
            return JsonConvert.DeserializeObject<IEnumerable<FileContent>>(JsonConvert.SerializeObject(datas));
        }

        ICommand _selectTargetDirCmd;
        public ICommand SelectTargetDirCmd
        {
            get
            {
                if (_selectTargetDirCmd == null)
                {
                    _selectTargetDirCmd = new DelegateCommand(SelectTargetDirAction);
                }
                return _selectTargetDirCmd;
            }
        }

        private void SelectTargetDirAction(object obj)
        {
            FolderBrowserDialog folderDia = new FolderBrowserDialog();
            DialogResult rst = folderDia.ShowDialog();
            if(rst==DialogResult.Cancel)
            {
                return;
            }
            TargetDir = folderDia.SelectedPath;
            AppSettingUtils.Update(Key_TargetDir, TargetDir);
        }

        ICommand _submitCmd;
        public ICommand SubmitCmd
        {
            get
            {
                if (_submitCmd == null)
                {
                    _submitCmd = new DelegateCommand(SubmitAction);
                }
                return _submitCmd;
            }
        }

        private void SubmitAction(object obj)
        {
            if(TargetDir.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "请选择目标目录");
                return;
            }
            if(!Directory.Exists(TargetDir))
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "目标目录不存在");
                return;
            }
            DirectoryInfo dirTarget = new DirectoryInfo(TargetDir);
            //1、删除列表中不存在的文件
            //1.1清空
            if(FileContents.IsEmpty())
            {
                //将要清空目标目录
                dirTarget.Delete(true);
                dirTarget.Create();
                return;
            }
            //1.2删除部分多余文件
            var lstNames = FileContents.Select(fc => fc.filecode + fc.filename);
            var filesToDel = dirTarget.GetFiles().Where(f => !lstNames.Contains(f.Name.Substring(0, f.Name.IndexOf('.'))));
            if(filesToDel.IsNotEmpty())
            {
                filesToDel.ToList().ForEach(f => f.Delete());
            }

            //2、新建文件
            var filesToAdd = lstNames.Where(n => !dirTarget.GetFiles().Select(f => f.Name.Substring(0, f.Name.IndexOf('.'))).Contains(n));
            if(filesToAdd.IsNotEmpty())
            {
                filesToAdd.ToList().ForEach(name => File.Create(TargetDir.TrimEnd('/', '\\')+"/"+name + ".docx"));
            }
            //3、修改文件

            MessageWindow.ShowMsg(MessageType.Info, "提示", "修改成功");

            if(OpenAfterSubmit)
            {
                Process.Start("Explorer.exe", TargetDir);
            }
        }
    }
}
