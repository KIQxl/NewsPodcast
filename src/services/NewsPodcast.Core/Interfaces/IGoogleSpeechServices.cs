namespace NewsPodcast.Core.Interfaces
{
    public interface IGoogleSpeechServices
    {
        public byte[] SpeechText(string text);
    }
}
