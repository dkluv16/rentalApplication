using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CampChetekRental.Models
{
    public class RentalAttribute : DbContext
    {
        public RentalAttribute(DbContextOptions<RentalAttribute> options)
            : base(options)
        { }

        public DbSet<Register> register { get; set; }
        public DbSet<GroupType> groupTypes { get; set; }
        public DbSet<ActivityType> activityTypes { get; set; }
        public DbSet<TimeOfYear> timeOfYears { get; set; }
        public DbSet<ProgramChoice> programs { get; set; }
        public DbSet<MealType> mealTypes { get; set; }
        public DbSet<MealChoice> mealChoices { get; set; }
        public DbSet<HousingType> housingTypes { get; set; }
        public DbSet<HousingChoice> housingChoices { get; set; }
        public DbSet<BeddingType> beddingTypes { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<BlockDates> blockDates { get; set; }

        public string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public static User CreateNewUser(int id, string password, string firstName, string lastName, string email, int userRoleId)
        {
            var saltBytes = new byte[128 / 8];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);
           
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
           
            
            User hashSalt = new User {userId = id, FirstName = firstName, LastName = lastName, Email = email, userRoleId = userRoleId, Password = password, Hash = hashPassword, Salt = salt };
            return hashSalt;

          
        }
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
              CreateNewUser(1, "1234", "Dan", "Kluver", "danael@campchetek.org", 1)
             
              ) ;

            modelBuilder.Entity<Register>().HasData(
            new Register
            {
                registerId = 1,
                GroupTypeId = 3,
                event_start = DateTime.Now,
                event_end = DateTime.Now,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@campchetek.org",
                Phone = "715 924-3222",
                Address = "730 Lakeview Drive",
                City = "Chetek",
                State = "WI",
                Zip = 54728,
                Pet = true,
                FoodAllergies = "Peanut",
                GroupNumber = 33
                
              
            },
            new Register
            {
                registerId = 2,
                GroupTypeId = 2,
                event_start = DateTime.Now,
                event_end = DateTime.Now,
                FirstName = "Bill",
                LastName = "Summer",
                Email = "summer@campchetek.org",
                Phone = "815 324-4522",
                Address = "110th Ave South St.",
                City = "Barron",
                State = "WI",
                Zip = 54528,
                Pet = false,
                FoodAllergies = "Gluten",
                GroupNumber = 105
                
            }
                );
            modelBuilder.Entity<GroupType>().HasData(
                new GroupType
                {
                    GroupTypeId = 1,
                    TypeName = "Other"
                },
                new GroupType
                {
                    GroupTypeId = 2,
                    TypeName = "School Camp"
                },
                new GroupType
                {
                     GroupTypeId = 3,
                     TypeName = "Retreat"
                },
                new GroupType
                {
                     GroupTypeId = 4,
                     TypeName = "Day Camp"
                },
                new GroupType
                {
                    GroupTypeId = 5,
                    TypeName = "Ladies Retreat"
                },
                new GroupType
                {
                    GroupTypeId = 6,
                    TypeName = "Mens Retreat"
                },
                new GroupType
                {
                    GroupTypeId = 7,
                    TypeName = "Family Reunion"
                }
                );
            modelBuilder.Entity<ActivityType>().HasData(
                new ActivityType
                {
                    activityTypeId = 1,
                    name = "Swim Area",
                    description = "With over 500 feet of sandy beach, full size artificial palm trees," +
                    " white coral rock and floating inflatables," +
                    " you can take a tropical vacation or day trip right here in Wisconsin",
                    timeOfYearId = 1,
                    cost = 25,
                    isActive = true
                },
                new ActivityType
                {
                    activityTypeId = 2,
                    name = "Horse Back Riding",
                    description = "Are you ready to start “horsin’ around?”" +
                    " Camp is the summer home to 14 horses – who all seem to earn" +
                    " a place in the heart of someone each summer. The riding director" +
                    " and her capable staff work to ensure a safe and quality" +
                    " environment for equestrian instruction, from novice to advanced skills.",
                    timeOfYearId = 1,
                    cost = 30,
                    isActive = true
                },
                new ActivityType
                {
                    activityTypeId = 3,
                    name = "High Ropes",
                    description = "The High Ropes Course is a unique, intense," +
                    " and fun experience! Participants will learn about faith, trust," +
                    " overcoming fears, and teamwork. One of the unique aspects of" +
                    " this course is that participants must work together as a team." +
                    "  This makes the course perfect for groups such as junior high" +
                    " and high school groups, sports teams, and colleagues. ",
                    timeOfYearId = 2,
                    cost = 35,
                    isActive = true
                },
                new ActivityType
                {
                    activityTypeId = 4,
                    name = "Water Tubing",
                    description = "Tubing is a great way to take in all that the outdoors has to offer while staying cool on the water. ",
                    timeOfYearId = 2,
                    cost = 15,
                    isActive = true
                },
                new ActivityType
                {
                    activityTypeId = 5,
                    name = "Laser Tag",
                    description = "Laser Tag is for the adventure enthusiast!" +
                    " Campers love this thrilling game of hide-n-seek in our" +
                    " Outdoor Laser Tag facility, while competing in both team games and advanced competitions.",
                    timeOfYearId = 3,
                    cost = 5,
                    isActive = true
                },
                new ActivityType
                {
                    activityTypeId = 6,
                    name = "Escape Room",
                    description = "Escape rooms are live-action team-based" +
                    " games where players discover clues, solve puzzles," +
                    " and accomplish tasks in one or more rooms in order" +
                    " to accomplish a specific goal in a limited amount of time.",
                    timeOfYearId = 4,
                    cost = 10,
                    isActive = true
                }

                );
            modelBuilder.Entity<TimeOfYear>().HasData(
                new TimeOfYear
                {
                    timeOfYearId = 1,
                    startDate = DateTime.Now,
                    endDate = DateTime.Now,
                    season = "Winter"
                },
                new TimeOfYear
                {
                     timeOfYearId = 2,
                     startDate = DateTime.Now,
                     endDate = DateTime.Now,
                     season = "Spring"
                },
                new TimeOfYear
                {
                    timeOfYearId = 3,
                    startDate = DateTime.Now,
                    endDate = DateTime.Now,
                    season = "Summer"
                },
                new TimeOfYear
                {
                    timeOfYearId = 4,
                    startDate = DateTime.Now,
                    endDate = DateTime.Now,
                    season = "Fall"
                }
                );
            modelBuilder.Entity<ProgramChoice>().HasData(
                new ProgramChoice
                {
                    programChoiceId = 1,
                    activityTypeId = 2,
                    numberParticipating = 35,
                    registerId = 1,

                    
                },
                new ProgramChoice
                {
                    programChoiceId = 2,
                    activityTypeId = 3,
                    numberParticipating = 45,
                    registerId = 1,
                   
                }
                );
            modelBuilder.Entity<MealChoice>().HasData(
                new MealChoice
                {
                    mealChoiceId = 1,
                    mealTypeId = 2,
                    numberOfMeals = 4,
                    registerId = 2,
                },
                new MealChoice
                {
                    mealChoiceId = 2,
                    mealTypeId = 3,
                    numberOfMeals = 6,
                    registerId = 1,

                }
                );
            modelBuilder.Entity<MealType>().HasData(
                new MealType
                {
                    mealTypeId = 1,
                    mealDescription = "Simple breakfast: with cereal and cinnamon rolls.",
                    cost = 5,
                    type = "Breakfast",
                    isActive = true,
                },
                new MealType
                {
                     mealTypeId = 2,
                     mealDescription = "Home cooked breakfast: with eggs, sausage, and bacon.",
                     cost = 8,
                     type = "Breakfast",
                     isActive = true,
                },
                new MealType
                {
                     mealTypeId = 3,
                    mealDescription = "Continental breakfast: with waffle bar, pancakes cooked to order, steak",
                     cost = 12,
                     type = "Breakfast",
                     isActive = true,
                },
                new MealType
                {
                    mealTypeId = 4,
                    mealDescription = "Simple lunch: with sandwich and water and lemonade",
                    cost = 3,
                    type = "Lunch",
                    isActive = true,
                },
                new MealType
                {
                     mealTypeId = 5,
                    mealDescription = "Cookout lunch: with hamburgers, chips, beans, and the fixens",
                     cost = 6,
                     type = "Lunch",
                     isActive = true,
                },
                new MealType
                {
                    mealTypeId = 6,
                    mealDescription = "Buffet lunch: with your choice of food options from pizza to prime rib",
                    cost = 15,
                    type = "Lunch",
                    isActive = true,
                },
                new MealType
                {
                     mealTypeId = 7,
                    mealDescription = "Simple dinner: with pasta and either fettuine alfredo or tomato sauce",
                     cost = 5,
                     type = "Dinner",
                     isActive = true,
                },
                new MealType
                {
                    mealTypeId = 8,
                    mealDescription = "Cookout dinner: with hamburgers, chips, beans, and the fixens",
                    cost = 6,
                    type = "Dinner",
                    isActive = true,
                },
                new MealType
                {
                     mealTypeId = 9,
                    mealDescription = "Banquet: with your choice of prime meat cut, and sides.",
                     cost = 13,
                     type = "Dinner",
                     isActive = true,
                }
                );
           
            modelBuilder.Entity<HousingType>().HasData(
                new HousingType
                {
                    housingTypeId = 1,
                    building = "Camper Cabin",
                    buildingDescription = "Roomy cabin with sleeping up to 11. Twin size bunk beds and A/C. Bathhouse is located in close proximity for showers and restroom.",
                    cost = 10,
                    type = "LodgingWithBedding",
                    isActive = true,
                },
                new HousingType
                {
                    housingTypeId = 2,
                    building = "Mondern Room",
                    buildingDescription = "Four seasons housing with A/C and Heat. Private Bathroom in room, sleeping up to 7. Three sets of twin size bunks and one queen bed.",
                    cost = 20,
                    type = "LodgingWithBedding",
                    isActive = true,
                },
                new HousingType
                {
                    housingTypeId = 3,
                    building = "RV Park",
                    buildingDescription = "The RV Park has plenty of room for any size RV. Hook ups for septic, water, and eletriity. Bathhouse located in RV park with showers, restroom, and laundry.",
                    cost = 7,
                    type = "Lodging",
                    isActive = true,
                },
                new HousingType
                {
                    housingTypeId = 4,
                    building = "Tent Camp Site",
                    buildingDescription = "Sleep under the stars, in our well mainted sites. Bathhouse located close by with showers, restroom, and laundry.",
                    cost = 4,
                    type = "Lodging",
                    isActive = true,
                },
                new HousingType
                {
                    housingTypeId = 5,
                    building = "Lodge",
                    buildingDescription = "The Lodge has meeting room for up to 70. Also includes kitchen with dining up to 90. Also includes games such as pool table, bumper pool tables, and carpet ball.",
                    cost = 45,
                    type = "Meeting Area",
                    isActive = true,
                },
                new HousingType
                {
                    housingTypeId = 6,
                    building = "Dining Hall Meeting Area",
                    buildingDescription = "Small meeting area for group up to 35, summer time use only, has A/C.",
                    cost = 30,
                    type = "Meeting Area",
                    isActive = true,

                },
                new HousingType
                {
                    housingTypeId = 7,
                    building = "Gym Class Room",
                    buildingDescription = "Meeting area for groups up to 150, includes comfortable setting and year-round use.",
                    cost = 50,
                    type = "Meeting Area",
                    isActive = true,
                }
                );
            modelBuilder.Entity<HousingChoice>().HasData(
                new HousingChoice
                {
                    housingChoiceId = 1,
                    housingTypeId = 2,
                    numberHousing = 7,
                    beddingTypeId = 1,
                    registerId = 1,
                }
                
                );
           

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { userRoleId = 1, userDescription = "Administrator" },
                new UserRole { userRoleId = 2, userDescription = "Hospitality" },
                new UserRole { userRoleId = 3, userDescription = "Cook Staff" },
                new UserRole { userRoleId = 4, userDescription = "New Account"}
                );

            modelBuilder.Entity<BlockDates>().HasData(
                new BlockDates { blockDatesId = 1, startDate = DateTime.Now, endDate = DateTime.Now},
                new BlockDates { blockDatesId = 2, startDate = DateTime.Now, endDate = DateTime.Now}
                );

            modelBuilder.Entity<BeddingType>().HasData(
                new BeddingType { beddingTypeId = 1, cost = 0, isBedding = false},
                new BeddingType { beddingTypeId = 2, cost = 15, isBedding = true}
                );
        } 
    }
}
