[TestMethod]
public void CheckStudentStatus_ShouldDisplayStatus()
{
    // Arrange
    var st = new SeniorTutor();

    // Act
    var status = st.CheckStudentStatus();

    // Assert
    Assert.IsNotNull(status, "Student status is not displayed for ST.");
}
