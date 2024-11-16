using System.Text;
using CafeConnect.Domain.Entities;
using CafeConnect.Domain.Repositories;

namespace CafeConnect.Infrastructure.Seeds
{
    public class EmployeeDataInitializer
    {
        private const string Prefix = "UI";
        private const int Length = 7;
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        private static readonly List<Employee> employees =
        [
            new Employee { Id = GenerateEmployeeId(), Name = "Alice Smith", EmailAddress = "alice.smith@example.com", PhoneNumber = 87289722, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Bob Johnson", EmailAddress = "bob.johnson@example.com", PhoneNumber = 64429350, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Charlie Brown", EmailAddress = "charlie.brown@example.com", PhoneNumber = 31062378, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Diana Prince", EmailAddress = "diana.prince@example.com", PhoneNumber = 17668551, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Edward Davis", EmailAddress = "edward.davis@example.com", PhoneNumber = 23993258, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Fiona Green", EmailAddress = "fiona.green@example.com", PhoneNumber = 19137758, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "George Harris", EmailAddress = "george.harris@example.com", PhoneNumber = 88241695, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Hannah Lee", EmailAddress = "hannah.lee@example.com", PhoneNumber = 74939717, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Ian Walker", EmailAddress = "ian.walker@example.com", PhoneNumber = 82840896, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Julia Adams", EmailAddress = "julia.adams@example.com", PhoneNumber = 07361383, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Kyle Martin", EmailAddress = "kyle.martin@example.com", PhoneNumber = 58996963, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Laura Nelson", EmailAddress = "laura.nelson@example.com", PhoneNumber = 91113703, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Mike Scott", EmailAddress = "mike.scott@example.com", PhoneNumber = 87145172, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Nina Patel", EmailAddress = "nina.patel@example.com", PhoneNumber = 87361383, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Oscar Martinez", EmailAddress = "oscar.martinez@example.com", PhoneNumber = 83923344, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Pamela Anderson", EmailAddress = "pamela.anderson@example.com", PhoneNumber = 69498058, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Quincy Adams", EmailAddress = "quincy.adams@example.com", PhoneNumber = 99287448, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Rachel Green", EmailAddress = "rachel.green@example.com", PhoneNumber = 87542242, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Sam Wilson", EmailAddress = "sam.wilson@example.com", PhoneNumber = 85980281, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Tina Brown", EmailAddress = "tina.brown@example.com", PhoneNumber = 91353642, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Ursula Grant", EmailAddress = "ursula.grant@example.com", PhoneNumber = 97145172, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Victor Lee", EmailAddress = "victor.lee@example.com", PhoneNumber = 95980281, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Wendy Hill", EmailAddress = "wendy.hill@example.com", PhoneNumber = 86366852, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Xander Cole", EmailAddress = "xander.cole@example.com", PhoneNumber = 87361383, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Yvonne Moore", EmailAddress = "yvonne.moore@example.com", PhoneNumber = 91113703, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Zachary Hughes", EmailAddress = "zachary.hughes@example.com", PhoneNumber = 91353642, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Amy Turner", EmailAddress = "amy.turner@example.com", PhoneNumber = 91113703, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "Ben Lewis", EmailAddress = "ben.lewis@example.com", PhoneNumber = 85745072, Gender = Domain.Enums.GenderType.Male },
            new Employee { Id = GenerateEmployeeId(), Name = "Clara Scott", EmailAddress = "clara.scott@example.com", PhoneNumber = 81353642, Gender = Domain.Enums.GenderType.Female },
            new Employee { Id = GenerateEmployeeId(), Name = "David King", EmailAddress = "david.king@example.com", PhoneNumber = 95980281, Gender = Domain.Enums.GenderType.Male}
        ];
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICafeRepository _cafeRepository;

        public EmployeeDataInitializer(IEmployeeRepository employeeRepository, ICafeRepository cafeRepository)
        {
            _employeeRepository = employeeRepository;
            _cafeRepository = cafeRepository;
        }

        public async Task SeedAsync()
        {
            Random random = new();
            if (!await _employeeRepository.EmployeesExistsAsync())
            {
                List<Cafe> cafes = await _cafeRepository.GetAllAsync();

                foreach (var emp in employees)
                {
                    // Get a random index
                    int randomIndex = random.Next(cafes.Count);
                    Cafe randomCafe = cafes[randomIndex];
                    Guid tempCafeId = randomCafe.Id;

                    int randomDays = random.Next(maxValue: 10);
                    emp.CafeId = tempCafeId;
                    emp.StartedAt = DateTime.Now.AddDays(-randomDays);

                    await _employeeRepository.InsertAsync(emp);
                }
            }
        }

        private static string GenerateEmployeeId()
        {
            var random = new Random();
            var result = new StringBuilder();

            for (int i = 0; i < Length; i++)
                result.Append(Characters[random.Next(Characters.Length)]);

            return $"{Prefix}{result}";
        }

    }
}