using Google.Cloud.TextToSpeech.V1;
using Microsoft.Extensions.Configuration;
using NewsPodcast.Core.Interfaces;

namespace NewsPodcast.Infraestructure.Services
{
    public class GoogleSpeechService : IGoogleSpeechServices
    {
        public GoogleSpeechService(IConfiguration config)
        {
            var credentialsPath = config["Google:CredentialsPath"];
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);
        }
        public byte[] SpeechText(string text)
        {
            if (string.IsNullOrEmpty(text) || text is null)
                return null;

            var client = TextToSpeechClient.Create();

            var input = new SynthesisInput { Text = text };

            var voiceSelection = new VoiceSelectionParams
            {
                LanguageCode = "pt-br",
                SsmlGender = SsmlVoiceGender.Female,
                Name = "pt-BR-Wavenet-C"
            };

            var audioConfig = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Linear16
            };

            try
            {
                var response = client.SynthesizeSpeech(input, voiceSelection, audioConfig);

                return response.AudioContent.ToByteArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
