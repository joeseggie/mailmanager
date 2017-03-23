namespace MailManager.Models
{
    /// <summary>
    /// Office mail object.
    /// </summary>
    public class OfficeMail
    {
        /// <summary>
        /// Mail reference number.
        /// </summary>
        public string ReferenceNumber { get; set; }
        /// <summary>
        /// Sender of the mail.
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Recipient of the mail.
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Subject of the mail.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Record version
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}