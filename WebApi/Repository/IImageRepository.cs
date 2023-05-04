using WebApi.Models.Domain;

namespace WebApi.Repository;

public interface IImageRepository
{
    Task<Image> Upload(Image image);
}
