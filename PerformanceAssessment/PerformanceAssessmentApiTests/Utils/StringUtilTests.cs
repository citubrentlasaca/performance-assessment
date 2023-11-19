using PerformanceAssessmentApi.Utils;
using System.Globalization;

namespace PerformanceAssessmentApiTests.Utils
{
    public class StringUtilTests
    {
        [Fact]
        public void GetCurrentDateTime_WhenCalled_ReturnsFormattedDateTimeString()
        {
            // Arrange (minimal, as no complex setup is needed)

            // Act
            string result = StringUtil.GetCurrentDateTime();

            // Assert
            // Check if the result is not null or empty
            Assert.False(string.IsNullOrWhiteSpace(result));

            // Check if the result follows a specific format (e.g., "yyyy-MM-dd HH:mm:ss")
            Assert.Matches(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$", result);

            // Check if the result can be parsed as a valid DateTime
            Assert.True(DateTime.TryParseExact(result, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out _));
        }

        [Fact]
        public void GetCurrentDateTime_WhenCalled_ThrowsFormatException()
        {
            // Arrange (minimal, as no complex setup is needed)

            // Act
            string result = StringUtil.GetCurrentDateTime();

            // Assert
            Assert.Throws<FormatException>(() => DateTime.ParseExact(result, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        }
    }
}