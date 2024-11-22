[TestMethod]
public void ScheduleMeeting_ShouldConfirmForPS()
{
    // Arrange
    var ps = new PersonalSupervisor();
    DateTime meetingDate = DateTime.Now.AddDays(2);

    // Act
    bool isMeetingScheduled = ps.ScheduleMeeting(meetingDate);

    // Assert
    Assert.IsTrue(isMeetingScheduled, "Meeting was not scheduled for PS.");
}
