import { defineStore } from "pinia";
import axios from "axios";
axios.defaults.headers.common = {
  Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
};

class Note {
  constructor(id, name, description, noteListId, completed = false) {
    this.id = id;
    this.description = description;
    this.name = name;
    this.completed = completed;
    this.noteListId = noteListId;
  }
}

export const useNoteStore = defineStore("noteStore", {
  state: () => ({
    notes: [],
  }),
  actions: {
    async refreshNotesFromServer(noteListId) {
      await axios
        .get(`/api/Notes/getNotesByNoteList/${noteListId}`)
        .then((response) => {
          // this.notes = response.data;
          // console.log(response.data);
          let merged = [...this.notes, ...response.data].reduce((acc, item) => {
            // Находим объект с таким же id и обновляем его
            let existingItem = acc.find(i => i.id === item.id);
            if (existingItem) {
              existingItem.value = item.value;
            } else {
              // Если объекта с таким id нет, добавляем новый
              acc.push(item);
            }
            return acc;
          }, []);
          this.notes = merged;
        });
      return noteListId;
    },
    async changeNoteStatus(id, currentStatus) {
      axios
        .put(`/api/Notes/updateCheckbox/${id}`, { completed: !currentStatus })
        .then(() => {
          this.notes[this.notes.findIndex((note) => note.id == id)].completed =
            !currentStatus;
        });
    },
    removeNote(id) {
      axios.delete(`/api/Notes/${id}`).then(() => {
        this.notes.splice(
          this.notes.findIndex((note) => note.id == id),
          1
        );
      });
    },
    getNoteList(id) {
      return this.notes.find((note) => note.id == id);
    },
    getNotesById(id) {
      const Notes = this.getNoteList(id);
      return Notes ? Notes.childrenNotesLists : [];
    },
    async createNote(name, description, noteListId) {
      //let id = this.notes.length;
      let newNote;
      await axios
        .post("/api/Notes", { name, description, position: 0, noteListId })
        .then((response) => {
          newNote = new Note(
            response.data.id,
            response.data.name,
            response.data.description,
            response.data.noteListId,
            response.data.completed
          );
          this.notes.push(newNote);
        })
        .catch((error) => {
          alert(
            error.response.data.errors[
              Object.keys(error.response.data.errors)[0]
            ]
          );
          throw "noteListCreationError";
        });
      // const newNote = new Note(id, name, text);
      // this.notes.push(newNote);
      return newNote;
    },
    async updateNote(note) {
      console.log(note);
      axios
        .put(`/api/Notes/${note.id}`, note)
        .then(
          () =>
            (this.notes[this.notes.findIndex((el) => el.id == note.id)] = note)
        );
    },
  },
});
