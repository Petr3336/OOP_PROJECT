﻿using System.ComponentModel.DataAnnotations;

namespace NotesService.Models.RequestModels
{
    public class CreateOrModifyNoteListRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public int Position { get; set; }
    }
}