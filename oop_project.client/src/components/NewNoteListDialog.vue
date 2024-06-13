<template>
  <v-card
    prepend-icon="mdi-note"
    :title="
      (editingNoteList ? 'Изменить' : 'Создать новый') + ' список заметок'
    "
  >
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
            v-model="newNoteList.description"
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
            @click="$emit('dialogClose')"
          ></v-btn>

          <v-btn
            color="primary"
            text="Создать"
            variant="tonal"
            type="submit"
            @click="
              createNewNoteList(
                form.isValid.value,
                newNoteList.name,
                newNoteList.description
              )
            "
          ></v-btn>
        </v-card-actions>
      </template>
    </v-form>
  </v-card>
</template>

<script>
import { useNavigationStore } from "@/stores/NavigationStore";
import { useNotesListStore } from "@/stores/NotesListsStore";
export default {
  name: "NewNoteListDialog",
  props: {
    editingNoteList: {
      required: false,
    },
  },
  setup() {
    const noteListStore = useNotesListStore();
    const navigationStore = useNavigationStore();
    return { noteListStore, navigationStore };
  },
  data: () => ({
    newNoteList: { name: "", description: "" },
    newNoteListRules: [
      (value) => !!value || "У списка должно быть название",
      (value) =>
        (value && value.length >= 3) ||
        "Минимальная длинна названия - 3 символа",
    ],
  }),
  mounted() {
    if (this.editingNoteList) {
      this.newNoteList = this.editingNoteList;
    }
  },
  methods: {
    createNewNoteList(isValid, newNoteListName, newNoteListText) {
      if (isValid) {
        this.noteListStore
          .createNotesList(newNoteListName, newNoteListText)
          .then(this.$emit("dialogClose"));
        this.newNoteList = { name: "", text: "" };
      }
    },
  },
};
</script>
