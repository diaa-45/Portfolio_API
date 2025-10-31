using Portfolio_API.DTOs;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;

namespace Portfolio_API.Services
{
        public class AboutService : IAboutService
        {
            private readonly IAboutRepository _aboutRepo;
            private readonly IImageService _imageService;

            public AboutService(IAboutRepository aboutRepo, IImageService imageService)
            {
                _aboutRepo = aboutRepo;
                _imageService = imageService;
            }

            public async Task<About?> GetAbout()
            {
                var about = await _aboutRepo.GetAbout();
                return about;
            }

            public async Task<About> CreateAsync(CreateAboutDto dto)
            {
                // Check if a record already exists
                var existing = await GetAbout();
                if (existing != null)
                {
                    throw new InvalidOperationException("About record already exists. Use Update instead.");
                }
                // add image if provided
                string? openingImage = null;

                if (dto.OpeningImage != null)
                    openingImage = await _imageService.UploadSingleImageAsync(dto.OpeningImage, "Abouts");


                var about = new About
                {
                    OpeningImage = openingImage,
                    OpeningText = dto.OpeningText,
                    Title = dto.Title,
                    Description = dto.Description,
                    Mission = dto.Mission,
                    Vision = dto.Vision,
                    Values = dto.Values
                };

                return await _aboutRepo.AddAsync(about);
            }

            public async Task<About?> UpdateAsync(UpdateAboutDto dto)
            {
                var existing = await _aboutRepo.GetAbout();
                if (existing == null)
                {
                    throw new InvalidOperationException("About record does not exist. Create one first.");
                }

                existing.OpeningText = dto.OpeningText ?? existing.OpeningText;
                existing.Title = dto.Title ?? existing.Title;
                existing.Description = dto.Description ?? existing.Description;
                existing.Mission = dto.Mission ?? existing.Mission;
                existing.Vision = dto.Vision ?? existing.Vision;
                existing.Values = dto.Values ?? existing.Values;
                existing.UpdatedAt = DateTime.UtcNow;
                if (dto.OpeningImage != null)
                {
                    if (!string.IsNullOrEmpty(existing.OpeningImage))
                        await _imageService.DeleteImageAsync(existing.OpeningImage);
                    existing.OpeningImage = await _imageService.UploadSingleImageAsync(dto.OpeningImage, "Abouts");
                }

                return await _aboutRepo.UpdateAsync(existing);
            }

        }
}
