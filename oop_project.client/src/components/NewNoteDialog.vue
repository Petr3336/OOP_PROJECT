<template>
  <v-dialog max-width="900" v-model="newNoteDialog">
    <template v-slot:activator="{ props: activatorProps }">
      <v-list-item
        prepend-icon="mdi-note-plus"
        v-bind="activatorProps"
        v-tooltip="'Создать новую заметку'"
      ></v-list-item>
    </template>
    <v-card prepend-icon="mdi-note" title="Создать новую заметку">
      <v-form @submit.prevent>
        <template v-slot:default="form">
          <v-card-text>
            <v-text-field
              label="Имя новой заметки*"
              required
              :rules="newNoteRules"
              class="mb-2"
              color="primary"
              v-model="newNote.name"
            ></v-text-field>

            <v-textarea
              label="Содержание заметки"
              color="primary"
              v-model="newNote.text"
              counter
              auto-grow
              no-resize
            ></v-textarea>

            <small class="text-caption text-medium-emphasis"
              >* Обозначает необходимое для заполнения поле</small
            >
          </v-card-text>

          <v-divider></v-divider>

          <v-card-actions>
            <v-spacer></v-spacer>

            <v-btn
              text="Отмена"
              variant="plain"
              @click="newNoteDialog = false"
            ></v-btn>

            <v-btn
              color="primary"
              text="Создать"
              variant="tonal"
              type="submit"
              @click="
                createNewNote(form.isValid.value, newNote.name, newNote.text)
              "
            ></v-btn>
          </v-card-actions>
        </template>
      </v-form>
    </v-card>
  </v-dialog>
</template>

<script>
import { useNoteStore } from "@/stores/NoteStore";
import { useNotesListStore } from "@/stores/NotesListsStore";
export default {
  name: "NewNoteDialog",
  data: () => ({
    newNote: {
      name: "",
      text: "",
    },
    newNoteDialog: false,
    newNoteRules: [
      (value) => !!value || "У заметки должен быть заголовок",
      (value) =>
        (value && value.length >= 3) ||
        "Минимальная длинна названия - 3 символа",
    ],
  }),
  methods: {
    createNewNote(isValid, newNoteName, newNoteText) {
      if (isValid) {
        this.notesStore.createNote(newNoteName, newNoteText, this.$route.params.id);
        this.newNoteDialog = false;
        this.newNote = { name: "", text: "" };
      }
    },
  },
  setup() {
    const notesStore = useNoteStore();
    const notesListsStore = useNotesListStore();
    return { notesStore, notesListsStore };
  },
};
</script>
