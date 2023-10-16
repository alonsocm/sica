using Shared.Utilities.Services;

namespace Core.Tests.Infrastructure.Utilities
{
    public class MetadataExtractorServiceTest
    {
        [Fact]
        public void GetMetadataFromImage()
        {
            string path = "D:\\Pictures\\IMG_4902.JPG";

            if (File.Exists(path))
            {
                using FileStream stream = File.OpenRead(path);
                Assert.NotNull(MetadataExtractorService.GetMetadaFromImage(stream));
            }
        }
    }
}
