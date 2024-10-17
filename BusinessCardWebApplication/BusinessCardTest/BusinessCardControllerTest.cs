using BusinessCard_API.Controllers;
using BusinessCard_Core.Dtos.BusinessCardDtos;
using BusinessCard_Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static BusinessCard_Core.Helpers.Enums.ApplicationLookups;

namespace BusinessCardTest
{
    public class BusinessCardControllerTest
    {
        private readonly Mock<IBusinessCardService> _mockBusinessCardService;
        private readonly BusinessCardController _businessCardController;

        public BusinessCardControllerTest()
        {
            _mockBusinessCardService = new Mock<IBusinessCardService>();
            _businessCardController = new BusinessCardController(_mockBusinessCardService.Object);
        }

        [Fact(DisplayName = "Create New Business Card")]
        public async Task Create_New_BusinessCardAsync_ShouldReturnOk()
        {
            CreateBusinessCardDTO dto = new CreateBusinessCardDTO
            {
                Name = "Test Name",
                Email = "Test Email",
                Gendear = "Male",
                Phone = "0000000000",
                Address = "Test Address",
                DateOfBirth = new DateOnly(2024, 01, 01),
                Photo = "data:image/webp;base64, iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAABU1BMVEVYsOD/////6L5ncHlGRElDSVXm5ubpVz7exJL53KRTrt9Prd9Gqt5Zs+T/8MRmb3hKUVxTWmVFPkD/67xobHJFOztFOTj/3qE0O0k7Qk/m7e7t9vu83fFtueM9PET/8cVYYm2azOuNxug5OUIyMz+uu7Sq1O7T6fbJ4/Tj8fmUvs8wN0bpTC+Bwebu2bOj0ezpSixTl71RiKnx1Z5bpM1jgpdhiqRMcIhLZ3tOepbEs5eLgXPhzapJWmn137ibj31pY17SwKFUUFHKzrl0t9j5362nxMfX0rPf3MaIi5H++fFflLOlp6zm1NHnyMTpZlGlmIO2p45nYVx9dGq3pIHMtYyMf2uEu9IYHzO9yr/x48G0zc+rubGOpaumppc+T2D/9djcv4mbnqPx5tPMztDX19m1t7vztKfwn5T76ufujoDse2n0wrvrcF1jd4boOxHok4g2z1YtAAATDUlEQVR4nM2d6UMTZx7HZ3KRzJEEASEJCYFkORRQAlUOwYqiBVGq7LZbu91WVLRe2///1T7zzPVcc/2eZ7DfN92F+Mx8+N3PTGY0PX91lpZnF+cX1rrdrq1pmo3+u7Ywvzi7vNS5gqNreS7eWZqd72qmZZmmaSBpnpz/baCfoV9o3fnZfEHzIuws3+oaiIDgEspBtSyje2s5L8w8CDvLC5pjtVg0BhR9XlvIhVI54dKtrmO5DHShOU2rO7+k+oTUEi4vmFYm2/GUlrmwrPScFBIiPCm6gNJUCqmKcGVeDZ4HaRm3VhSdmRrC2a6lDs8VislZJeemgLCzqNJ8oZC3LipIrtKEKyj6csBzhSJS2lklCVfWlLsnLcNak2SUIsydz2WUs6MEYecq+FzGNYl4hBPeuiI+l/HWlRPOGvnlF5FMA1o7YIQr3avlw4xdWDiCCOev0EFDGdb8FREuaVdvQFemBpg8shMufBMDukKVI3fClW9mQFemljUaMxIufkMDujKsxRwJO98ghfIyu5nqfxbCJdDmhHoZRpaEk4Hw23uor0yemp5wzfrWYISsNeWEf48QDJU+GFMSrigJQTuU9FqGkbJspCNckg1BTLW1sTEa7ezsjEajjS1NltOw0uWbVISzUiHokGzsbDenkK55mpq6fm3/7sjhhC9spRo30hDKACK8jZ3mNYRW4oRAS/ujLThkKsQUhItwQIR3tzQlgCMwr++PDChkmqqRTAgHtLVRMx7Pg5za3gAypkBMJAQD2tpOKQWeb0ggYzJiEiEUEPGlMV+oqeYGDDEpFhMIoUnGHqW2X8i4bUAYkxDjCZdhgPbW/lRWvpITjyOIq1rxF6piCZeAgCNRbUhlxuYWBDG29McRroBaUVvbhhjQZxwBEM24Bi6OUIO0avZWGWhAD3EnO6IRSxH9qy4IcEOKz0HcBiB2IYRrEB+1NyQ81NO1/ezHNaPnxUhCUCG0N65LAyLEZvYjR1f+KEJQGlVhQYwIsGJkQo0g7MCSjGwMBoiQWIwY+iMIQVnGUMRXAmXUqGwjJrwFyjJNVSZ0ELN3qab4GqOQEBaEdxUCIsStzGcgDkUhIaTUq0mjhPaz+6kYRvCzBVC3ppYP1L+ZoitTAkLQQKHYRx1dy+5JojFDQAjaONzyfLTXk0cbeITZS4ZhpCGcB+XRbdeEgx/PB9KAu97/uJ59lDLnkwlXQD665TYzvfO5+t5Qiq/Xuzd3z1sie7LRLG6Q4ghhE8W+a8LDQqEwtzuQ8NTh+UG9UH/hIgKKIl/3WcJZUB71onD4oI4Q6+svh2AD7jorFOov8f+FNG8mu23DEoLSjJdIe+f49NAJ7pYg0dgb7h14K3h+ej172dfYZMMQgto1TXN9dLhe8FQvvOhlZUR863V/gblz7OnXAAM/27zRhB3Q1pM98kw4VyiEjLsvh+njsTcY3A/5QiOWIBtTnRjCBZiT7rsmvFcgVa/f2yulguwNB+e7hTr9r7/HvwLkGs1YiyYEVQq0pFsqDulzxIZ8cP9wOIjrAnqDYWlv96DO/tv6Lvbya3chRlyJJFyDmdB10sELjhBb8mD3/vel4QBxUqA95JjD3su9F+t1Dg8L7qa0EUlCoAm9fibMMzxlYf3B7o9754elwRCrd/jy/P6L3fVCBJ3zj9xcA8mmtBFJQpgJNdv9Yx/ORZyrj4k0N4cLJvoPVuznPTeF7BBTRiQIgSb0yn3vfuwJZ9c6dlNI0aeNSBDCEmkQhg/UAhbqh9g1mhBCY0FE2AHeMOM1NIMD1YR7OBCnQH93oiaGhItQwv2IWiFL+GIArYhUYxMSQu95chNNb081odfWgFINQuQJYUMF0lZMNZTTEFzzyREjIATNhZhwKpdE46caWDIl5kSfEFoq/GsVTFOqhNCt+aBkShQMn3AeakKvWAyVAxbq93vQvg3JmGcIwXfmeYSl+I4GROgm0+vA+6UMmnAZfPeovYMJX6pPNIUHLiHwxPy9U48Q2JIGhMEGhkq55QJK6Denmlwx9Fsa5V2pI7czvQ7945skIdxJfcIfcyA8gM9PmHCZIAQ23SFhHgW/UCjJeKnffmuSTurF4WA3B8K6HKHnppgQeHdX7oSHcoTuFVNMCNwldQlxPcyhaQsIwTdJuwOGJtWT5kz4UqKn0fze1CGE7QP7hPg2r2EuhN/34H2pIzwHazKDk0s4lU/j7RMCLrH5wiOUJlcrNG96yokQPh9i4XrhEILusgyV0/DkEwIuzgSEmksI3YLyZJfzJRxJnJ7ZwYQSLRuS8cQZVPPLNMPqDXgidBo3Ta4aaub69E+9PKvFYHP6MRjRqYiaXDW0Hk7X/jnIs+If1mrTj6Bn6FRETaopNR5N12rVYY5dW++8Vqs9gRvRIZSp944Ja7XcCPFYVpMxIqr5mr4kk6uO0PFrL3Ocngb/cggfQ8/RXEKE0N18R13HhDWUanKbgIebDuFDqJ+Zi4hQoqPBYVhzUk1uuxiHzgHggYi6Gk0mlRo/Y0KUanK4bIF3ono/YcJ1MGEXEYL5AsLa4SCXvbYHg97wX3KEGuKTLhZIhRelknrC+u6/9+4dyXkpKhca+IKFIzfT1Cbq9Txy6YP1egGvD880mrWiyRQLr1rUjgqFPIqFI5cQXC1QudCkxl/riU+Yl1zCn8HZ0FyWIzR/8QIxZ0KJM5zVpCYLP5nmBiibaFDJ18AXDrGsWr6EsmGoGfOa3CaN56a5BaKsk6KmRoNfV8MLPMqfcFrCSTVjTZOZf7Ugm+YEeCSZSZ22TetKAfpGzJNQxoQKCJERp/Nz05rU+Isly+cox0B0AH+RewKX/POaNPPGdF5u6jjp+t/gCVyWUzFyIXS8QzITqpGzH5WLm9ama4/+BiZEsh5P52HEo+knKp4SpyLXaKZ24wf1hA8fKXiOoXy18GTdmFANOKHEQ7uyPY0vo6uc8JWKBzWiii/Xl4ayVLvpxK8qTIj6UrnZglhKtZtOKPEuNFvIzYeETLWAhR+UPE0UzYdSMz4p8xelRpy4oeTE0IwvtU9Dynik1k3VPBDWnFVHqDbXqMkzeK9Nar+UkvGzQiOqyTN4v1Rqz5tZ7ZU6QvguNy1rReq6BSPzsTIjTsiNvcQ5yV174pZTBaioVDiSu37IyvxVkREnZDafSOHrh6qaGiw1gOpMiK8By1zHZ6XIiMqi0L2Or65cIFlKvmepLgrxvRhS989yK6rov1XVQs27n0ZhuXCWlK+JE5L7h6RM6fvaOKnoThWeTVf+3kRO1kNJREVDBZZ3b6Lc/aWsjK6kBdWlmeD+UqWpRrp3U5hm3Jv1FdznzS0rM0VN/Krw7+3f5622q5H0UyUbbMGZLKj4voVAloSfKk3swfctFAeiZtyAXsVQWQo14jszuq3YTW9sAgmP/qPSnQw7+GaXsh1Fb+Ub01UY4et/KCWcV/L9Q9HKiBCEWFVLSHz/UG1r6hBuVgGhWFVMSHyHVHG9QIS1anbEqmJC6nvAihs3RLiZGbGqmtAiv8ut1k0dQmTEbIhV5YTU9/HVuim+OwMZsZph4K8qJ/SfFeU/F0NhNrXN35z7T6pZrFj19F9Lwf0vrpjnYsCfbcLJ3vr93CHczIDoA1bvz4BexCIS82wT2NODBbK1u73Wufs1DKwUfEcBYPWnVm8f8iIWXsEThaWfMUTL3iiXy63iHwRiYjCGfK/XT1vlcm9HhRm5Zwwp2a2xje2eQzipv6n5flqtxjep6wTgK/0CEZZLv8Ne/USKf06UghHKtkelMta4rp8chYgx0XhQJQDf6PpxCy/Quyt7x53gWV/SJdHe2u+5gOWms16BQIxwVSIAkU70gBAJ9pi2kFDnCeV2921tx+dDwgu+miYQq5scJMX3+h7+NyFhb1sm4wifuSf1pXWcYQK13BXfUIiktx7Q1sMhiDXZCleRyTjC5yZK9TXbPQGhflJjEKPkhCDWOLlM73fo44XEz76UKBj2iAIsz7T9NY+CohGrE//z1DLlEvSJERHPLwU/g1Z72qfP7GYlWPMV66kCA1bDU5ihF2reAQVj1DNooS8NuDM52WQI34WLvplOcNXXB8QpMITlxuR3gPeURj5HOLsRbXvru8lGscgQzrwlFj2p1WIYN6f/ID7bvskSFhuTX7O+3jL6WdBZjWjbG18dvmKROa+ZS3JV3f16lgDS+Z72CfnJCk9YRIzFp5kagJjneWdKpyj8ii5fsRFPiIPRhdz0MTc33W8uUoD6O9ZL++76k40MAWnQb5uBPlff1u40PL5ikUk05dYFTaj/4SPSmi4wn3vLEo57RyhmCMjY5+qnvJboh18xivCYOXMvGFnAV+zHLiMJi6kDMv7dCKkm4TD8fI2zhEX21INgJAHfcB86bTErNcnDpAvIhPdbJI8YRPgFYlIpCh+eUC+wnnrCf+aYJSzTB0oRkInvKEmYE6nwC8Seltd6M6KD8Uj0kSJHyB0rISCT3zMTVzHY8AsOygHeFJ0+GYx8CGL1OcK+4HBxAZniXUGROzZ8+MUQtrllMaIfjFSZJ8S5u4gQM0YEZJr3PUVc9BaFny82ldJtGyUvGAUhiMUWfCqZ0oyNOwJG0bvl0r13TRx+vthUyrRtlJxgPIoC5Jo2JplSmhQEZMr3rnHvzosIvzjCS8Gyrk5qbJkP9Y4nLMccFyUdOiDTvjuP8dPI8AvERw/X1BB6/2fkr7iWJp7QC8hYH03zDsutBD5RseCbmkAfbo9FIp4KCBMO3ph8GlgxwzssyebN/i4JkE+lzo5phD7eHhtbfRbxywuuWEQkU/LoRZ8wy3tIqbo/mXAIEaG7nyjQp9UxpNvPxb/lC34yYXHS2z7O9i7Z8H3A9tNEQr5YhHtRjM4wIEJ8L/w1n7IiywXxB/6KCY2M7wMOQtEuJjmpIJVGlPw/xwLd/iA6qMCEMeUiMOJWdBDGEHrv5bY3Ek0oJJwRlPw/V8cIxI/8B9qCRJOQTLER79iQ93J771ZPzjOiYiEsiM9vj5Fa/cR9gtvDwEo+gQbs3eputtlKNmGxycqJw1N2tc80IEL8wn4El8NxVsmEk0+jskwCoW4Y9p3k9Tk5XssVxPcsoCOmMLoTfmLy5IRyTQxFHOGKqWUHdGsHWxA//08AuDpGf8ibfxOzJ3fIvniUSSbUl1LkGUZ+2qHLRaX9QWTDZxXqU345bGb8u/ajJplkQv0yo8s0gqxDzcCVSqXNuenqGfoxhRimrEyH7UcOMikIMyIStZ+4dOEAIkQm1ax+wT8mEYlUmsGM/eg5Jg2hfpoBkSyMxIToklTaz8l6uPqxXWEQ6WKR9rhJgImE6RHp9jQsFxVf7WdEwf+rHf7c+yA9O7XSJZxEwGTCtIhMZxOUiwqpM9+CH9rETz1EbrM0hacmA6YgTBWLDbaxablbpm0KsP3FJ3xO/dz1VH6zNPHAKQDTEKZAFIwXTR6wUgmi8HOFRxQtEm/GNICpCBFi/JGiposKq4DwPcuus1e4k83YSCgTWQj1t3GIogkYbyhygM/8grH6F0tYEW20YW+PBowt9BkJ9Uocomi4QNMFB1gJ68VHjrAi2oYqRw9PjUYl+bQzEOrtmElYaMTWMU/4OSD8whNyqTTWSxtfI0Z6MCFKddEhITJia5JjaL8Paz5PyKfScvSE34/ezIMTosIYZUaxETmI9l8B4Spv4EkRofiIjVRJNDthTL4RJtN3HOGnsGl7xhGKAMV9TcokCiDU21+jEAVnN/OWIzwLCG8/Z38p2tEXm7D/NW4clCPU9YsIMwqqdeuUIwzDkCv5wlQqMmGjz22QKCXU3zXEiALCC84Rw/mJL/miVCoCbKSrgnBCJ6eKGHkj8sn0GUH4gSUUpFI+eTf6MZd8lBHqb4Vm5CvGOEtIDohsyW8LUqnAgFlSDJxQHI18xZhhw5Aoh2OfWBsmX+CGGBBKqL8r8ozcKbLJtP2BIDxjCLn7vdhi3+gXU7ZpSgjRuMG5KmdEtjNtfyR2McYYE/KptMEAZijySggFrsrdGcV0pkTBRwWRIeRS6TjNd5G2DVVIqLeZrMoasVVkCM9IQqap4VJpg+Q7zlTjlRFyjFzFYGKNdFJ2H4NNpeMkHywAVRAixt/6RECygch4IrlhuvqZwm+zJiT4spZ4tYS6vvJbIzAkY0QmmT6jCJmSz3SlfTX2U0Ko653LoHbQFYPpTD9TO8L0PgaTSpsuXv9UIv4UEiK9u2hgZ2V2halkShV8tuQzqbSP6x+0PtBSQ4gC8vLYiUjKiHRnShV8tuTTqbSJzHch7Z6eVBEiVU6Lfeb2SYqQKvjMPgaZSlsz/WNA/xklhYRI7y77N4lzpcb8YMPbC0QqRgm8GUXe6UstIdLbi/GbMx4l1be1aRNS+xhuV9qaudlSaT1Xygl1HJNNRNlikukqTUiW/MsZRFcuXqqKPVJ5EDpqv71o3LxJJtNnDCG5j3ExfpwLnaO8CLEqb9thPmHup7nt72O02x1wV51GuRJ66nTaSM+/fDnzeu+zs08fP37udPJF8/R//FC/0Pr2Se4AAAAASUVORK5CYII=",
            };

            _mockBusinessCardService.Setup(service => service.CreateBusinessCardAsync(dto))
                                    .ReturnsAsync(true);
            var result = await _businessCardController.CreateBusinessCardAsync(dto);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Get All Business Cards")]
        public async Task Get_All_BusinessCardAsync_ShouldReturnOk()
        {
            _mockBusinessCardService.Setup(service => service.GetAllBusinessCardAsync())
                                    .ReturnsAsync(new List<BusinessCardRecordDTO>());
            var result = await _businessCardController.GetAllBusinessCardAsync();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Get All Business Card By Filter")]
        public async Task Filter_GetAll_BusinessCardAsync_ShouldReturnNotFoundForInvalidInput()
        {
            string invalidInput = null;
            _mockBusinessCardService.Setup(service => service.SearchOnBusinessCard(invalidInput))
                                    .ReturnsAsync(new List<BusinessCardRecordDTO>());
            var result = await _businessCardController.FilterOnBusinessCard(invalidInput);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Delete Business Card")]
        public async Task Delete_BusinessCard_ShouldReturnOk()
        {
            int validId = 0;
            _mockBusinessCardService.Setup(service => service.DeleteBusinessCard(validId))
                                   .ReturnsAsync(true);

            var result = await _businessCardController.DeleteBusinessCard(validId);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
