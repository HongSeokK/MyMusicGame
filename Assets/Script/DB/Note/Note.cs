using SQLite4Unity3d;

namespace MusicGame.DB
{
    [Table("m_note")]
    public sealed class NoteTable
    {
        [PrimaryKey, Column("id")]
        public int ID { get; set; } = 0;

        [Column("type")]
        public string Type { get; set; } = string.Empty;

        [Column("create_time")]
        public int CreateTime { get; set; } = 0;

    }
}
