usin[TestMethod]
public void AccessSystem_ShouldLoadCorrectly()
{
    // Arrange
    var system = new System();

    // Act
    bool isSystemAccessible = system.LoadOnSchoolComputer();

    // Assert
    Assert.IsTrue(isSystemAccessible, "System did not load correctly on school computer.");
}
