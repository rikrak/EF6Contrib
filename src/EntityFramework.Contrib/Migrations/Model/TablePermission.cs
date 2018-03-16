namespace System.Data.Entity.Migrations.Model
{
    /// <summary>
    /// The SQL Grant permission enumeration
    /// </summary>
    public enum TablePermission
    {
        /// <summary>
        /// Identify the Select permission
        /// </summary>
        Select = 0,
        /// <summary>
        /// Identify the Update permission
        /// </summary>
        Update = 1,
        /// <summary>
        /// Identify the Delete permission
        /// </summary>
        Delete = 2,
        /// <summary>
        /// Identify the Insert permission
        /// </summary>
        Insert = 4,
        /// <summary>
        /// Identify the alter permission
        /// </summary>
        Alter = 8,
        /// <summary>
        /// Identify the control permission
        /// </summary>
        Control = 16,
    }
}
