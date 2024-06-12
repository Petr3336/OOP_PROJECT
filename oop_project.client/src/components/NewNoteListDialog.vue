<template>
  <v-dialog max-width="600" v-model="newNoteListDialog">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        flat
        size="38px"
        icon="mdi-note-plus"
        v-tooltip="'Создать новый список заметок'"
        v-bind="activatorProps"
      >
      </v-btn>
    </template>
    <v-card prepend-icon="mdi-note" title="Создать новый список заметок">
      <v-form @submit.prevent>
        <template v-slot:default="form">
          <v-card-text>
            <v-text-field
              label="Название вашего списка заметок*"
              required
              :rules="newNoteListRules"
              v-model="newNoteList.name"
              class="mb-2"
              color="primary"
            ></v-text-field>

            <v-text-field
              label="Описание"
              color="primary"
              v-model="newNoteList.text"
            ></v-text-field>

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
              @click="newNoteListDialog = false"
            ></v-btn>

            <v-btn
              color="primary"
              text="Создать"
              variant="tonal"
              type="submit"
              @click="
                createNewNoteList(form.isValid.value, newNoteList.name, newNoteList.text)
              "
            ></v-btn>
          </v-card-actions>
        </template>
      </v-form>
    </v-card>
  </v-dialog>
</template>

<script>
import { useNavigationStore } from "@/stores/NavigationStore";
import { useNotesListStore } from "@/stores/NotesListsStore";
export default {
  name: "NewNoteListDialog",
  setup() {
    const noteListStore = useNotesListStore();
    const navigationStore = useNavigationStore();
    return { noteListStore, navigationStore };
  },
  data: () => ({
    newNoteList: { name: "", text: "" },
    newNoteListDialog: false,
    newNoteListRules: [
      (value) => !!value || "У списка должно быть название",
      (value) =>
        (value && value.length >= 3) ||
        "Минимальная длинна названия - 3 символа",
    ],
  }),
  methods: {
    createNewNoteList(isValid, newNoteListName, newNoteListText) {
      if (isValid) {
        let newNoteList = this.noteListStore.createNotesList(newNoteListName, newNoteListText);
        this.navigationStore.createNoteNavigation(newNoteList.id, newNoteListName);
        this.newNoteListDialog = false;
        this.newNoteList = { name: "", text: "" };
      }
    },
  },
};
</script>
