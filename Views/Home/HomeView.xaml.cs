using olhuz_desktop_completo.ViewModels.Home;

#if WINDOWS
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
#endif

#if IOS || MACCATALYST
using Foundation;
using UIKit;
#endif

namespace olhuz_desktop_completo.Views.Home
{
    public partial class HomeView : ContentPage
    {
        private readonly HomeViewModel _viewModel;


        public HomeView(HomeViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            BindingContext = _viewModel;
        }


        // =========================================================
        // QUANDO A TELA É ABERTA
        // =========================================================

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Busca os dados do usuário logado
            await _viewModel.LoadUserDataAsync();
        }


        // =========================================================
        // ARRASTAR ARQUIVO SOBRE O CARD
        // =========================================================

        private void OnDragOver(object sender, DragEventArgs e)
        {
            // Define que o arquivo será copiado
            e.AcceptedOperation =
                Microsoft.Maui.Controls.DataPackageOperation.Copy;

            // Altera o visual do card
            UploadCard.Stroke =
                Color.FromArgb("#326FE8");

            UploadCard.BackgroundColor =
                Color.FromArgb("#E8F1FF");
        }


        // =========================================================
        // ARQUIVO SAIU DO CARD
        // =========================================================

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            ResetUploadCard();
        }


        // =========================================================
        // ARQUIVO FOI SOLTO SOBRE O CARD
        // =========================================================

        private async void OnFileDropped(
            object sender,
            DropEventArgs e)
        {
            ResetUploadCard();

            string? filePath = null;


            // ==========================================
            // WINDOWS
            // ==========================================

#if WINDOWS

            var dragEventArgs =
                e.PlatformArgs?.DragEventArgs;

            if (dragEventArgs?.DataView != null &&
                dragEventArgs.DataView.Contains(
                    StandardDataFormats.StorageItems))
            {
                var items =
                    await dragEventArgs.DataView
                        .GetStorageItemsAsync();

                var file =
                    items
                        .OfType<StorageFile>()
                        .FirstOrDefault();

                filePath = file?.Path;
            }

#endif


            // ==========================================
            // IOS / MACCATALYST
            // ==========================================

#if IOS || MACCATALYST

            var session =
                e.PlatformArgs?.DropSession;

            if (session != null)
            {
                foreach (UIDragItem item in session.Items)
                {
                    var provider =
                        item.ItemProvider;

                    var typeIdentifiers =
                        provider
                            .RegisteredTypeIdentifiers
                            .ToList();

                    var fileUrl =
                        await LoadFileAsync(
                            provider,
                            typeIdentifiers);

                    if (fileUrl != null)
                    {
                        filePath = fileUrl.Path;
                        break;
                    }
                }
            }

#endif


            // ==========================================
            // PROCESSAR ARQUIVO
            // ==========================================

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                await _viewModel.ProcessFileAsync(
                    filePath);
            }
        }


        // =========================================================
        // RESTAURA O CARD
        // =========================================================

        private void ResetUploadCard()
        {
            UploadCard.Stroke =
                Color.FromArgb("#A8CFFF");

            UploadCard.BackgroundColor =
                Color.FromArgb("#F1F7FF");
        }


        // =========================================================
        // IOS / MACCATALYST
        // =========================================================

#if IOS || MACCATALYST

        private static async Task<NSUrl?>
            LoadFileAsync(
                NSItemProvider provider,
                List<string> typeIdentifiers)
        {
            if (typeIdentifiers.Count == 0)
                return null;

            string typeIdentifier =
                typeIdentifiers[0];

            if (provider.HasItemConformingTo(
                typeIdentifier))
            {
                return await provider
                    .LoadFileRepresentationAsync(
                        typeIdentifier);
            }

            typeIdentifiers.RemoveAt(0);

            return await LoadFileAsync(
                provider,
                typeIdentifiers);
        }

#endif
    }
}