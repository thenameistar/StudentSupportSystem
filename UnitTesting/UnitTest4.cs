[TestMethod]
public void DataConsistency_ShouldRemainAcrossDevices()
{
    // Arrange
    var system = new System();
    var initialData = system.GetData();

    // Simulate data access from another device
    var otherDeviceData = system.GetDataFromAnotherDevice();

    // Assert
    Assert.AreEqual(initialData, otherDeviceData, "Data is inconsistent across devices.");
}
