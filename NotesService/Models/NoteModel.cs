﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesService.Models
{
    // Модель заметки
    public class NoteModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public UserModel User { get; set; }

        [Required]
        [ForeignKey("NoteListId")]
        public NoteListModel NoteList { get; set; }
    }
}
