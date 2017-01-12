using FileManager;
using Microsoft.Win32;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using MyNet.Components.Office;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using Novacode;
using NPOI.XWPF.UserModel;
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
    public partial class MainViewModel : BaseModel
    {
        const string Key_MngFile = "last_mng_file";
        const string Key_TempFile = "last_temp_file";
        const string Key_TargetDir = "last_target_dir";
        const string Key_DelNotList = "del_not_list";
        const string Key_OpenAfterSubmit = "open_after_submit";
        public MainViewModel()
        {
            MngFile = AppSettingUtils.Get(Key_MngFile);
            TempFile = AppSettingUtils.Get(Key_TempFile);
            TargetDir = AppSettingUtils.Get(Key_TargetDir);
            DelFileNotList = Convert.ToBoolean(AppSettingUtils.Get(Key_DelNotList));
            OpenAfterSubmit = Convert.ToBoolean(AppSettingUtils.Get(Key_OpenAfterSubmit));
        }

        private IEnumerable<FileContent> _fileCnts;
        public IEnumerable<FileContent> FileContents
        {
            get { return _fileCnts; }
            set
            {
                if (_fileCnts != value)
                {
                    _fileCnts = value;
                    base.RaisePropertyChanged("FileContents");
                }
            }
        }
        private string _mngFile;
        public string MngFile
        {
            get { return _mngFile; }
            set
            {
                if (_mngFile != value)
                {
                    _mngFile = value;
                    base.RaisePropertyChanged("MngFile");
                }
            }
        }
        private string _tempFile;
        public string TempFile
        {
            get { return _tempFile; }
            set
            {
                if (_tempFile != value)
                {
                    _tempFile = value;
                    base.RaisePropertyChanged("TempFile");
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
                if (_selectMngFileCmd == null)
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
            if (rst.HasValue && rst == true)
            {
                MngFile = openFileDia.FileName;
                AppSettingUtils.Update(Key_MngFile, MngFile);
            }
        }
        ICommand _selectTempFileCmd;
        public ICommand SelectTempFileCmd
        {
            get
            {
                if (_selectTempFileCmd == null)
                {
                    _selectTempFileCmd = new DelegateCommand(SelectTempFileAction);
                }
                return _selectTempFileCmd;
            }
        }

        private void SelectTempFileAction(object obj)
        {
            Microsoft.Win32.OpenFileDialog openFileDia = new Microsoft.Win32.OpenFileDialog();
            openFileDia.Filter = "Word Files|*.doc;*.docx";
            var rst = openFileDia.ShowDialog();
            if (rst.HasValue && rst == true)
            {
                TempFile = openFileDia.FileName;
                AppSettingUtils.Update(Key_TempFile, TempFile);
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
            if (MngFile.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "请选择管理文件");
                return;
            }
            if (!File.Exists(MngFile))
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "管理文件不存在");
                return;
            }
            IEnumerable<FileContent> fileCnts = ReadMngFile(MngFile);
            FileContents = fileCnts;
        }

        private IEnumerable<FileContent> ReadMngFile(string filename)
        {
            var colNames = new string[] { "filename", "filecode", "versioncode", "efdate", "writer", "remark", "draftdept", "checkdept1", "checkdept2", "checkdept3", "approver", "logo" };
            var cols = new Dictionary<int, string>();
            for (int idx = 0; idx < colNames.Length; idx++)
            {
                cols.Add(idx, colNames[idx]);
            }
            var datas = ExcelUtils.ReadFile(filename: filename, cols: cols);
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
            if (rst == DialogResult.Cancel)
            {
                return;
            }
            TargetDir = folderDia.SelectedPath;
            AppSettingUtils.Update(Key_TargetDir, TargetDir);
        }

        ICommand _clearTargetDirCmd;
        public ICommand ClearTargetDirCmd
        {
            get
            {
                if (_clearTargetDirCmd == null)
                {
                    _clearTargetDirCmd = new DelegateCommand(ClearTargetDirAction);
                }
                return _clearTargetDirCmd;
            }
        }

        private void ClearTargetDirAction(object obj)
        {
            if (TargetDir.IsEmpty())
            {
                return;
            }
            var rst = MessageWindow.ShowMsg(MessageType.Ask, "询问", "清空目标目录将不能恢复，是否继续？");
            if (rst.HasValue == false || rst == false)
            {
                return;
            }
            if (Directory.Exists(TargetDir))
            {
                Directory.Delete(TargetDir, true);
            }
            Directory.CreateDirectory(TargetDir);
            MessageWindow.ShowMsg(MessageType.Info, "提示", "目标目录已清空");
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

    }
}
