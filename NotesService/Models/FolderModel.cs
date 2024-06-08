﻿using System.ComponentModel.DataAnnotations;

namespace NotesService.Models
{
    public class FolderModel
    {
        public int Id { get; set; }
        public string FolderName { get; set; } = "NewFolder";
        public List<int>? FolderIds { get; set; }
        public List<int>? NoteIds { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}