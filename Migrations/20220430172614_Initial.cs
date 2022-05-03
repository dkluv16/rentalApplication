using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CampChetekRental.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "beddingTypes",
                columns: table => new
                {
                    beddingTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isBedding = table.Column<bool>(nullable: false),
                    cost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beddingTypes", x => x.beddingTypeId);
                });

            migrationBuilder.CreateTable(
                name: "blockDates",
                columns: table => new
                {
                    blockDatesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startDate = table.Column<DateTime>(nullable: false),
                    endDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blockDates", x => x.blockDatesId);
                });

            migrationBuilder.CreateTable(
                name: "groupTypes",
                columns: table => new
                {
                    GroupTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groupTypes", x => x.GroupTypeId);
                });

            migrationBuilder.CreateTable(
                name: "housingTypes",
                columns: table => new
                {
                    housingTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    building = table.Column<string>(nullable: true),
                    buildingDescription = table.Column<string>(nullable: true),
                    cost = table.Column<int>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    dateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_housingTypes", x => x.housingTypeId);
                });

            migrationBuilder.CreateTable(
                name: "mealTypes",
                columns: table => new
                {
                    mealTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mealDescription = table.Column<string>(nullable: true),
                    cost = table.Column<int>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    dateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mealTypes", x => x.mealTypeId);
                });

            migrationBuilder.CreateTable(
                name: "timeOfYears",
                columns: table => new
                {
                    timeOfYearId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startDate = table.Column<DateTime>(nullable: false),
                    endDate = table.Column<DateTime>(nullable: false),
                    season = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timeOfYears", x => x.timeOfYearId);
                });

            migrationBuilder.CreateTable(
                name: "userRoles",
                columns: table => new
                {
                    userRoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRoles", x => x.userRoleId);
                });

            migrationBuilder.CreateTable(
                name: "register",
                columns: table => new
                {
                    registerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    GroupTypeId = table.Column<int>(nullable: false),
                    GroupNumber = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Zip = table.Column<int>(nullable: false),
                    FoodAllergies = table.Column<string>(nullable: true),
                    Pet = table.Column<bool>(nullable: false),
                    event_start = table.Column<DateTime>(nullable: false),
                    event_end = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_register", x => x.registerId);
                    table.ForeignKey(
                        name: "FK_register_groupTypes_GroupTypeId",
                        column: x => x.GroupTypeId,
                        principalTable: "groupTypes",
                        principalColumn: "GroupTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "activityTypes",
                columns: table => new
                {
                    activityTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    timeOfYearId = table.Column<int>(nullable: false),
                    cost = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    dateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activityTypes", x => x.activityTypeId);
                    table.ForeignKey(
                        name: "FK_activityTypes_timeOfYears_timeOfYearId",
                        column: x => x.timeOfYearId,
                        principalTable: "timeOfYears",
                        principalColumn: "timeOfYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(nullable: true),
                    Hash = table.Column<string>(nullable: true),
                    UserPassword = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true),
                    userRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                    table.ForeignKey(
                        name: "FK_users_userRoles_userRoleId",
                        column: x => x.userRoleId,
                        principalTable: "userRoles",
                        principalColumn: "userRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "housingChoices",
                columns: table => new
                {
                    housingChoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    housingTypeId = table.Column<int>(nullable: false),
                    numberHousing = table.Column<int>(nullable: false),
                    beddingTypeId = table.Column<int>(nullable: false),
                    registerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_housingChoices", x => x.housingChoiceId);
                    table.ForeignKey(
                        name: "FK_housingChoices_beddingTypes_beddingTypeId",
                        column: x => x.beddingTypeId,
                        principalTable: "beddingTypes",
                        principalColumn: "beddingTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_housingChoices_housingTypes_housingTypeId",
                        column: x => x.housingTypeId,
                        principalTable: "housingTypes",
                        principalColumn: "housingTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_housingChoices_register_registerId",
                        column: x => x.registerId,
                        principalTable: "register",
                        principalColumn: "registerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mealChoices",
                columns: table => new
                {
                    mealChoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numberOfMeals = table.Column<int>(nullable: false),
                    mealTypeId = table.Column<int>(nullable: false),
                    registerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mealChoices", x => x.mealChoiceId);
                    table.ForeignKey(
                        name: "FK_mealChoices_mealTypes_mealTypeId",
                        column: x => x.mealTypeId,
                        principalTable: "mealTypes",
                        principalColumn: "mealTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mealChoices_register_registerId",
                        column: x => x.registerId,
                        principalTable: "register",
                        principalColumn: "registerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "programs",
                columns: table => new
                {
                    programChoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    activityTypeId = table.Column<int>(nullable: false),
                    numberParticipating = table.Column<int>(nullable: false),
                    registerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programs", x => x.programChoiceId);
                    table.ForeignKey(
                        name: "FK_programs_activityTypes_activityTypeId",
                        column: x => x.activityTypeId,
                        principalTable: "activityTypes",
                        principalColumn: "activityTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_programs_register_registerId",
                        column: x => x.registerId,
                        principalTable: "register",
                        principalColumn: "registerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "beddingTypes",
                columns: new[] { "beddingTypeId", "cost", "isBedding" },
                values: new object[,]
                {
                    { 1, 0, false },
                    { 2, 15, true }
                });

            migrationBuilder.InsertData(
                table: "blockDates",
                columns: new[] { "blockDatesId", "endDate", "startDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 4, 30, 12, 26, 14, 17, DateTimeKind.Local).AddTicks(991), new DateTime(2022, 4, 30, 12, 26, 14, 17, DateTimeKind.Local).AddTicks(490) },
                    { 2, new DateTime(2022, 4, 30, 12, 26, 14, 17, DateTimeKind.Local).AddTicks(1499), new DateTime(2022, 4, 30, 12, 26, 14, 17, DateTimeKind.Local).AddTicks(1469) }
                });

            migrationBuilder.InsertData(
                table: "groupTypes",
                columns: new[] { "GroupTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Other" },
                    { 2, "School Camp" },
                    { 3, "Retreat" },
                    { 4, "Day Camp" },
                    { 5, "Ladies Retreat" },
                    { 6, "Mens Retreat" },
                    { 7, "Family Reunion" }
                });

            migrationBuilder.InsertData(
                table: "housingTypes",
                columns: new[] { "housingTypeId", "building", "buildingDescription", "cost", "dateUpdated", "isActive", "type" },
                values: new object[,]
                {
                    { 6, "Dining Hall Meeting Area", "Small meeting area for group up to 35, summer time use only, has A/C.", 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Meeting Area" },
                    { 5, "Lodge", "The Lodge has meeting room for up to 70. Also includes kitchen with dining up to 90. Also includes games such as pool table, bumper pool tables, and carpet ball.", 45, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Meeting Area" },
                    { 4, "Tent Camp Site", "Sleep under the stars, in our well mainted sites. Bathhouse located close by with showers, restroom, and laundry.", 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Lodging" },
                    { 7, "Gym Class Room", "Meeting area for groups up to 150, includes comfortable setting and year-round use.", 50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Meeting Area" },
                    { 2, "Mondern Room", "Four seasons housing with A/C and Heat. Private Bathroom in room, sleeping up to 7. Three sets of twin size bunks and one queen bed.", 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "LodgingWithBedding" },
                    { 1, "Camper Cabin", "Roomy cabin with sleeping up to 11. Twin size bunk beds and A/C. Bathhouse is located in close proximity for showers and restroom.", 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "LodgingWithBedding" },
                    { 3, "RV Park", "The RV Park has plenty of room for any size RV. Hook ups for septic, water, and eletriity. Bathhouse located in RV park with showers, restroom, and laundry.", 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Lodging" }
                });

            migrationBuilder.InsertData(
                table: "mealTypes",
                columns: new[] { "mealTypeId", "cost", "dateUpdated", "isActive", "mealDescription", "type" },
                values: new object[,]
                {
                    { 9, 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Banquet: with your choice of prime meat cut, and sides.", "Dinner" },
                    { 8, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Cookout dinner: with hamburgers, chips, beans, and the fixens", "Dinner" },
                    { 6, 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Buffet lunch: with your choice of food options from pizza to prime rib", "Lunch" },
                    { 5, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Cookout lunch: with hamburgers, chips, beans, and the fixens", "Lunch" },
                    { 7, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Simple dinner: with pasta and either fettuine alfredo or tomato sauce", "Dinner" },
                    { 3, 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Continental breakfast: with waffle bar, pancakes cooked to order, steak", "Breakfast" },
                    { 2, 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Home cooked breakfast: with eggs, sausage, and bacon.", "Breakfast" },
                    { 1, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Simple breakfast: with cereal and cinnamon rolls.", "Breakfast" },
                    { 4, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Simple lunch: with sandwich and water and lemonade", "Lunch" }
                });

            migrationBuilder.InsertData(
                table: "timeOfYears",
                columns: new[] { "timeOfYearId", "endDate", "season", "startDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 4, 30, 12, 26, 14, 15, DateTimeKind.Local).AddTicks(3686), "Winter", new DateTime(2022, 4, 30, 12, 26, 14, 15, DateTimeKind.Local).AddTicks(3181) },
                    { 2, new DateTime(2022, 4, 30, 12, 26, 14, 15, DateTimeKind.Local).AddTicks(4582), "Spring", new DateTime(2022, 4, 30, 12, 26, 14, 15, DateTimeKind.Local).AddTicks(4551) },
                    { 3, new DateTime(2022, 4, 30, 12, 26, 14, 15, DateTimeKind.Local).AddTicks(4616), "Summer", new DateTime(2022, 4, 30, 12, 26, 14, 15, DateTimeKind.Local).AddTicks(4611) },
                    { 4, new DateTime(2022, 4, 30, 12, 26, 14, 15, DateTimeKind.Local).AddTicks(4625), "Fall", new DateTime(2022, 4, 30, 12, 26, 14, 15, DateTimeKind.Local).AddTicks(4621) }
                });

            migrationBuilder.InsertData(
                table: "userRoles",
                columns: new[] { "userRoleId", "userDescription" },
                values: new object[,]
                {
                    { 3, "Cook Staff" },
                    { 1, "Administrator" },
                    { 2, "Hospitality" },
                    { 4, "New Account" }
                });

            migrationBuilder.InsertData(
                table: "activityTypes",
                columns: new[] { "activityTypeId", "cost", "dateUpdated", "description", "isActive", "name", "timeOfYearId" },
                values: new object[,]
                {
                    { 1, 25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "With over 500 feet of sandy beach, full size artificial palm trees, white coral rock and floating inflatables, you can take a tropical vacation or day trip right here in Wisconsin", true, "Swim Area", 1 },
                    { 2, 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Are you ready to start “horsin’ around?” Camp is the summer home to 14 horses – who all seem to earn a place in the heart of someone each summer. The riding director and her capable staff work to ensure a safe and quality environment for equestrian instruction, from novice to advanced skills.", true, "Horse Back Riding", 1 },
                    { 3, 35, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The High Ropes Course is a unique, intense, and fun experience! Participants will learn about faith, trust, overcoming fears, and teamwork. One of the unique aspects of this course is that participants must work together as a team.  This makes the course perfect for groups such as junior high and high school groups, sports teams, and colleagues. ", true, "High Ropes", 2 },
                    { 4, 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tubing is a great way to take in all that the outdoors has to offer while staying cool on the water. ", true, "Water Tubing", 2 },
                    { 5, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laser Tag is for the adventure enthusiast! Campers love this thrilling game of hide-n-seek in our Outdoor Laser Tag facility, while competing in both team games and advanced competitions.", true, "Laser Tag", 3 },
                    { 6, 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Escape rooms are live-action team-based games where players discover clues, solve puzzles, and accomplish tasks in one or more rooms in order to accomplish a specific goal in a limited amount of time.", true, "Escape Room", 4 }
                });

            migrationBuilder.InsertData(
                table: "register",
                columns: new[] { "registerId", "Address", "City", "Email", "FirstName", "FoodAllergies", "GroupNumber", "GroupTypeId", "LastName", "Pet", "Phone", "State", "Zip", "event_end", "event_start" },
                values: new object[,]
                {
                    { 2, "110th Ave South St.", "Barron", "summer@campchetek.org", "Bill", "Gluten", 105, 2, "Summer", false, "815 324-4522", "WI", 54528, new DateTime(2022, 4, 30, 12, 26, 14, 14, DateTimeKind.Local).AddTicks(6362), new DateTime(2022, 4, 30, 12, 26, 14, 14, DateTimeKind.Local).AddTicks(6325) },
                    { 1, "730 Lakeview Drive", "Chetek", "john@campchetek.org", "John", "Peanut", 33, 3, "Doe", true, "715 924-3222", "WI", 54728, new DateTime(2022, 4, 30, 12, 26, 14, 14, DateTimeKind.Local).AddTicks(971), new DateTime(2022, 4, 30, 12, 26, 14, 9, DateTimeKind.Local).AddTicks(9456) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "userId", "Email", "FirstName", "Hash", "LastName", "Password", "Salt", "UserEmail", "UserPassword", "userRoleId" },
                values: new object[] { 1, "danael@campchetek.org", "Dan", "19Dn6kMzpc+Tu4g/tiW2+tDE001i83A3zX3bdZJWOl9kFIYZIggxQ2q8xON2wGNEg5lqCUGiHAz+Kp2WtL/B8eBZzFxr1ckehd94rc5+j3h3CsSwi+TfPQ/34QYaTJq5ph3NrfOPvoli7HBWnsoyfZPGgSRaMbSz9ZF3qBX8+Dx2gzkG5l1X484UgK1t3JgZ8MlFpUvotZHEtbtWWu9Mx/RDRJesMArIzEZyfs0+TEkTHZ37/JZYv4RKErvNEHqhV9CEPVUzXQlIMb6n4cEAyfMCBnfrV1Y+Xl1r+6fUJ9cq3HwZbqgyYYRmK1P4mssWjZxM9Vefaiz1SkPUvbWEpA==", "Kluver", "1234", "/AkHj0yx/0sDzWQrrpBGuw==", null, null, 1 });

            migrationBuilder.InsertData(
                table: "housingChoices",
                columns: new[] { "housingChoiceId", "beddingTypeId", "housingTypeId", "numberHousing", "registerId" },
                values: new object[] { 1, 1, 2, 7, 1 });

            migrationBuilder.InsertData(
                table: "mealChoices",
                columns: new[] { "mealChoiceId", "mealTypeId", "numberOfMeals", "registerId" },
                values: new object[,]
                {
                    { 1, 2, 4, 2 },
                    { 2, 3, 6, 1 }
                });

            migrationBuilder.InsertData(
                table: "programs",
                columns: new[] { "programChoiceId", "activityTypeId", "numberParticipating", "registerId" },
                values: new object[,]
                {
                    { 1, 2, 35, 1 },
                    { 2, 3, 45, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_activityTypes_timeOfYearId",
                table: "activityTypes",
                column: "timeOfYearId");

            migrationBuilder.CreateIndex(
                name: "IX_housingChoices_beddingTypeId",
                table: "housingChoices",
                column: "beddingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_housingChoices_housingTypeId",
                table: "housingChoices",
                column: "housingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_housingChoices_registerId",
                table: "housingChoices",
                column: "registerId");

            migrationBuilder.CreateIndex(
                name: "IX_mealChoices_mealTypeId",
                table: "mealChoices",
                column: "mealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_mealChoices_registerId",
                table: "mealChoices",
                column: "registerId");

            migrationBuilder.CreateIndex(
                name: "IX_programs_activityTypeId",
                table: "programs",
                column: "activityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_programs_registerId",
                table: "programs",
                column: "registerId");

            migrationBuilder.CreateIndex(
                name: "IX_register_GroupTypeId",
                table: "register",
                column: "GroupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_users_userRoleId",
                table: "users",
                column: "userRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blockDates");

            migrationBuilder.DropTable(
                name: "housingChoices");

            migrationBuilder.DropTable(
                name: "mealChoices");

            migrationBuilder.DropTable(
                name: "programs");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "beddingTypes");

            migrationBuilder.DropTable(
                name: "housingTypes");

            migrationBuilder.DropTable(
                name: "mealTypes");

            migrationBuilder.DropTable(
                name: "activityTypes");

            migrationBuilder.DropTable(
                name: "register");

            migrationBuilder.DropTable(
                name: "userRoles");

            migrationBuilder.DropTable(
                name: "timeOfYears");

            migrationBuilder.DropTable(
                name: "groupTypes");
        }
    }
}
