using System;
using TextEditorBl;

namespace TextEditor
{
    class MainPresenter
    {
        private readonly IMainForm _view;
        private readonly IFileManager _manager;
        private readonly IMessageService _messageService;
        private string _currentFilePath;
        public MainPresenter(IMainForm view, IFileManager manager, IMessageService messageService)
        {
            _view = view;
            _manager = manager;
            _messageService = messageService;

            _view.SetSymvolCount(0);

            _view.ContentChanged += _view_ContentChanged;
            _view.FileOpenClick += _view_FileOpenClick;
            _view.FileSaveClick += _view_FileSaveClick;
        }

        void _view_FileSaveClick(object sender, EventArgs e)
        {
            try
            {
                string content = _view.Content;
                _manager.SaveContent(content, _currentFilePath);
                _messageService.ShowMessage("Файл сохранён успешно.");
            }
            catch(Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }
        void _view_FileOpenClick(object sender, EventArgs e)
        {
            try
            {
                string filePath = _view.FilePath;
                bool isExist = _manager.IsExist(filePath);

                if (!isExist)
                {
                    _messageService.ShowExclamation("Выбранный файл не существует");
                    return;
                }

                _currentFilePath = filePath;

                string content = _manager.GetContent(filePath);
                int count = _manager.GetSymvolCount(content);

                _view.Content = content;
                _view.SetSymvolCount(count);
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }
        void _view_ContentChanged(object sender, EventArgs e)
        {
            string content = _view.Content;
            int count = _manager.GetSymvolCount(content);
            _view.SetSymvolCount(count);
        }
    }
}
