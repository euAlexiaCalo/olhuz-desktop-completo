using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using olhuz_desktop_completo.Models.DTOs.Readings;
using olhuz_desktop_completo.Models.Responses;
using olhuz_desktop_completo.Models.Responses.Readings;

namespace olhuz_desktop_completo.Services
{
    public class ReadingService
    {
        private readonly HttpClient _httpClient;

        public ReadingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponse<List<ReadingHistoryResponse>>> GetReadingsAsync()
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<List<ReadingHistoryResponse>>>("api/readings");
        }

        public async Task<BaseResponse<ReadingHistoryResponse>> CreateReadingAsync(CreateReadingDto dto)
        {
            // Cria o formulário multipart para o envio de texto + arquivo
            using var content = new MultipartFormDataContent();

            // Adiciona os campos básicos de texto
            content.Add(new StringContent(dto.Type), "Type");
            content.Add(new StringContent(dto.Title), "Title");

            // Se a descrição for gerada pela IA após processar a imagem
            if (!string.IsNullOrEmpty(dto.DescriptionText) && dto.DescriptionText != "Nenhuma descrição disponível.")
            {
                content.Add(new StringContent(dto.DescriptionText), "DescriptionText");
            }

            // Verifica se há um arquivo selecionado e anexa à requisição
            if (!string.IsNullOrEmpty(dto.FilePath) && File.Exists(dto.FilePath))
            {
                var fileBytes = await File.ReadAllBytesAsync(dto.FilePath);
                var fileContent = new ByteArrayContent(fileBytes);

                // Mapeia o Content-Type correto de acordo com a extensão
                var extension = Path.GetExtension(dto.FilePath).ToLowerInvariant();
                var mimeType = extension switch
                {
                    ".pdf" => "application/pdf",
                    ".txt" => "text/plain",
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".webp" => "image/webp",
                    _ => "application/octet-stream"
                };

                // Define o Content-Type generico ou específico
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

                content.Add(fileContent, "File", Path.GetFileName(dto.FilePath));
            }

            // Faz o POST para a API
            var response = await _httpClient.PostAsync("api/readings", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BaseResponse<ReadingHistoryResponse>>();
            }

            return new BaseResponse<ReadingHistoryResponse>
            {
                Error = true,
                Message = $"Erro ao enviar arquivo para leitura ({response.StatusCode})."
            };
        }
    }
}