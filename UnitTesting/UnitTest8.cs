[TestMethod]
public void BookMeeting_ShouldConfirmForST()
{
    // Arrange
    var st = new SeniorTutor();
    DateTime meetingTime = DateTime.Now.AddDays(3);

    // Act
    bool isMeetingBooked = st.BookMeetingWithStudent(meetingTime);

    // Assert
    Assert.IsTrue(isMeetingBooked, "Meeting was not booked for ST.");
}
