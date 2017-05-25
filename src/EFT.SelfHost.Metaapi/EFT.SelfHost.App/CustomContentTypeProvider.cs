using Microsoft.Owin.StaticFiles.ContentTypes;

namespace EFT.SelfHost.App
{
    public class CustomContentTypeProvider : FileExtensionContentTypeProvider
    {
        public CustomContentTypeProvider()
        {
            Mappings.Add(".json", "application/json");
        }
    }
}
