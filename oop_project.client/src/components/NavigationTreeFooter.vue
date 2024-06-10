<template>
  <v-list-item
    class="mt-auto position-sticky bottom-0 left-0"
    height="64px"
    width="100%"
    :elevation="3"
    title=""
  >
    <template #prepend>
      <v-btn
        flat
        size="38px"
        icon
        v-tooltip="'Сменить тему'"
        @click="toggleTheme()"
      >
        <v-icon>mdi-brightness-6</v-icon>
      </v-btn>
    </template>
    <template #append>
      <v-dialog max-width="600" v-model="newFolderDialog">
        <template v-slot:activator="{ props: activatorProps }">
          <v-btn
            flat
            size="38px"
            icon
            v-tooltip="'Создать новую папку'"
            v-bind="activatorProps"
          >
            <v-icon>mdi-folder-plus</v-icon>
          </v-btn>
        </template>
        <v-card prepend-icon="mdi-folder" title="Создать новую папку">
          <v-card-text>
            <v-text-field
              label="Название вашей папки*"
              required
              :rules="newFolderRules"
              class="mb-2"
              color="primary"
            ></v-text-field>

            <v-text-field label="Описание" color="primary"></v-text-field>

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
              @click="newFolderDialog = false"
            ></v-btn>

            <v-btn
              color="primary"
              text="Создать"
              variant="tonal"
              @click="newFolderDialog = false"
            ></v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-dialog max-width="600" v-model="newNoteListDialog">
        <template v-slot:activator="{ props: activatorProps }">
          <v-btn
            flat
            size="38px"
            icon
            v-tooltip="'Создать новый список заметок'"
            v-bind="activatorProps"
          >
          <v-icon>mdi-note-plus</v-icon>
          </v-btn>
        </template>
        <v-card prepend-icon="mdi-note" title="Создать новый список заметок">
          <v-card-text>
            <v-text-field
              label="Название вашего списка заметок*"
              required
              :rules="newNoteListRules"
              class="mb-2"
              color="primary"
            ></v-text-field>

            <v-text-field label="Описание" color="primary"></v-text-field>

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
              @click="newFolderDialog = false"
            ></v-btn>

            <v-btn
              color="primary"
              text="Создать"
              variant="tonal"
              @click="newFolderDialog = false"
            ></v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </template>
  </v-list-item>
</template>

<script>
export default {
  name: "NavigationTreeFooter",
  data: () => ({
    newFolderDialog: false,
    newNoteListDialog: false,   
    newFolderRules: [
      (value) => !!value || "У папки должно быть название",
      (value) =>
        (value && value.length >= 3) ||
        "Минимальная длинна названия - 3 символа",
    ],
    newNoteListRules: [
      (value) => !!value || "У списка должно быть название",
      (value) =>
        (value && value.length >= 3) ||
        "Минимальная длинна названия - 3 символа",
    ],
  }),
  methods: {
    toggleTheme() {
      console.log(this.$vuetify.theme.global.name);
      this.$vuetify.theme.global.name = this.$vuetify.theme.global.current
        .dark
        ? "light"
        : "dark";
    },
  },
};
</script>
