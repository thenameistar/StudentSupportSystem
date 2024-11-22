[TestMethod]
public void CreateReport_ShouldSaveReport()
{
    // Arrange
    var student = new Student();
    string reportContent = "Test report content";

    // Act
    bool isReportSaved = student.CreateReport(reportContent);

    // Assert
    Assert.IsTrue(isReportSaved, "Report was not saved successfully.");
}
