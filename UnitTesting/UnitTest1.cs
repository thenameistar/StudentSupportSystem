[TestMethod]
public void BookMeeting_ShouldConfirmBooking()
{
    // Arrange
    var student = new Student();
    DateTime meetingTime = DateTime.Now.AddDays(1);

    // Act
    bool isMeetingBooked = student.BookMeeting(meetingTime);

    // Assert
    Assert.IsTrue(isMeetingBooked, "Meeting was not booked successfully.");
}
