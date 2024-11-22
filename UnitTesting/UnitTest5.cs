[TestMethod]
public void ViewStudentReports_ShouldDisplayReports()
{
    // Arrange
    var ps = new PersonalSupervisor();

    // Act
    var reports = ps.ViewStudentReports();

    // Assert
    Assert.IsNotNull(reports, "Student reports were not displayed.");
}
