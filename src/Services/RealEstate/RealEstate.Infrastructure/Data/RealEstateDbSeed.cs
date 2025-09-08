using System.Text.RegularExpressions;
using RealEstate.Domain.Entities.Blog;
using RealEstate.Domain.Entities.RealEstate;
using RealEstate.Domain.Entities.Setting;
using Shared.Enums;

namespace RealEstate.Infrastructure.Data
{
    public class RealEstateDbSeed
    {
        private RealEstateDbContext _dbContext;

        public RealEstateDbSeed(RealEstateDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SeedAsync()
        {
            var constants = new List<Constant>
            {
                new Constant { Type = ConstantType.Region, ConstantTitles = new List<ConstantTitle> { new ConstantTitle { Title = "North Region", Language = Language.English } } },
                new Constant { Type = ConstantType.Region, ConstantTitles = new List<ConstantTitle> { new ConstantTitle { Title = "South Region", Language = Language.English } } },
                new Constant { Type = ConstantType.Region, ConstantTitles = new List<ConstantTitle> { new ConstantTitle { Title = "East Region", Language = Language.English } } },
                new Constant { Type = ConstantType.Region, ConstantTitles = new List<ConstantTitle> { new ConstantTitle { Title = "West Region", Language = Language.English } } },
                new Constant { Type = ConstantType.Builder, ConstantTitles = new List<ConstantTitle> { new ConstantTitle { Title = "BuildCo", Language = Language.English } } },
                new Constant { Type = ConstantType.Builder, ConstantTitles = new List<ConstantTitle> { new ConstantTitle { Title = "ConstructIT", Language = Language.English } } },
                new Constant { Type = ConstantType.Builder, ConstantTitles = new List<ConstantTitle> { new ConstantTitle { Title = "MegaBuild", Language = Language.English } } },
                new Constant { Type = ConstantType.Tag, ConstantTitles = new List<ConstantTitle> { new ConstantTitle { Title = "Luxury", Language = Language.English } } },
                new Constant { Type = ConstantType.Tag, ConstantTitles = new List<ConstantTitle> { new ConstantTitle { Title = "Affordable", Language = Language.English } } },
                new Constant { Type = ConstantType.Tag, ConstantTitles = new List<ConstantTitle> { new ConstantTitle { Title = "Modern", Language = Language.English } } }
            };
            await _dbContext.AddRangeAsync(constants);
            var propertyInventories = new List<PropertyInventory>
            {
                new PropertyInventory
                {
                    StructureType = StructureType.Ready,
                    //RealEstateType = RealEstateType.TownHouse,
                    StartDate = "2025-01-01",
                    FinishDate = "2025-12-31",
                    Price = 500000,
                    TotalOfRooms = "3",
                    Capacity = "5",
                    Currency = Currency.AED,
                    RegionId = constants[0].Id,
                    BuilderId = constants[4].Id
                },
                new PropertyInventory
                {
                    StructureType = StructureType.Inprogress,
                    //RealEstateType = RealEstateType.Villa,
                    StartDate = "2025-03-01",
                    FinishDate = "",
                    Price = 1500000,
                    TotalOfRooms = "8",
                    Capacity = "10",
                    Currency = Currency.AED,
                    RegionId = constants[1].Id,
                    BuilderId = constants[5].Id
                },
                new PropertyInventory
                {
                    StructureType = StructureType.Ready,
                    //RealEstateType = RealEstateType.PentHouse,
                    StartDate = "2025-02-01",
                    FinishDate = "2025-08-01",
                    Price = 800000,
                    TotalOfRooms = "5",
                    Capacity = "7",
                    Currency = Currency.AED,
                    RegionId = constants[2].Id,
                    BuilderId = constants[6].Id
                }
            };

            await _dbContext.AddRangeAsync(propertyInventories);

            var propertyInventoryTitles = new List<PropertyInventoryTitle>
            {
                new PropertyInventoryTitle
                {
                    Language = Language.English,
                    Title = "3-Bedroom Apartment",
                    UrlTitle = Regex.Replace("3-Bedroom Apartment", @"\s+", " ").Replace(" ", "_"),
                    Description = "A luxurious 3-bedroom apartment in the North region.",
                    PaymentConditions = "50% upfront, 50% upon completion.",
                    PropertyInventoryId = propertyInventories[0].Id
                },
                new PropertyInventoryTitle
                {
                    Language = Language.English,
                    Title = "Luxury House",
                    UrlTitle = Regex.Replace("Luxury House", @"\s+", " ").Replace(" ", "_"),
                    Description = "An 8-bedroom house with modern amenities and large living spaces.",
                    PaymentConditions = "Installments over 6 months.",
                    PropertyInventoryId = propertyInventories[1].Id
                },
                new PropertyInventoryTitle
                {
                    Language = Language.English,
                    Title = "Office Space",
                    UrlTitle = Regex.Replace("Office Space", @"\s+", " ").Replace(" ", "_"),
                    Description = "A modern office with full facilities and ample space for your business.",
                    PaymentConditions = "Yearly payment in advance.",
                    PropertyInventoryId = propertyInventories[2].Id
                }
            };

           await _dbContext.AddRangeAsync(propertyInventoryTitles);

            var articles = new List<Article>
            {
                new Article { Title = "Investing in Real Estate",UrlTitle = Regex.Replace("Investing in Real Estate", @"\s+", " ").Replace(" ", "_"), Description = "A comprehensive guide to investing in the property market.", Language = Language.English, ArticleTags = new List<ArticleTag> { new ArticleTag { TagId = constants[7].Id } } },
                new Article { Title = "Tips for First-Time Homebuyers", UrlTitle = Regex.Replace("Tips for First-Time Homebuyers", @"\s+", " ").Replace(" ", "_"), Description = "Essential tips for people buying their first home.", Language = Language.English, ArticleTags = new List<ArticleTag> { new ArticleTag { TagId = constants[8].Id } } },
                new Article { Title = "How to Choose the Right Builder", UrlTitle = Regex.Replace("How to Choose the Right Builder", @"\s+", " ").Replace(" ", "_"), Description = "Learn how to select the best builders for your real estate projects.", Language = Language.English, ArticleTags = new List<ArticleTag> { new ArticleTag { TagId = constants[9].Id } } }
            };

            await _dbContext.AddRangeAsync(articles);

            var articleTags = new List<ArticleTag>
            {
                new ArticleTag { ArticleId = articles[0].Id, TagId = constants[7].Id },
                new ArticleTag { ArticleId = articles[1].Id, TagId = constants[8].Id },
                new ArticleTag { ArticleId = articles[2].Id, TagId = constants[9].Id }
            };

            await _dbContext.AddRangeAsync(articleTags);

            var contacts = new List<Contact>
            {
                new Contact { Name = "John Doe", Message = "Interested in buying a property.", Email = "johndoe@example.com", Phone = "+123456789" },
                new Contact { Name = "Jane Smith", Message = "Looking for an affordable house.", Email = "janesmith@example.com", Phone = "+987654321" },
                new Contact { Name = "Sam Brown", Message = "Seeking a real estate investment opportunity.", Email = "sambrown@example.com", Phone = "+555555555" }
            };

            await _dbContext.AddRangeAsync(contacts);
            await _dbContext.SaveChangesAsync();
        }
    }
}
