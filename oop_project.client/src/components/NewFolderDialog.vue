<template>
  <v-dialog max-width="600" v-model="newFolderDialog">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        flat
        size="38px"
        icon="mdi-folder-plus"
        v-tooltip="'Создать новую папку'"
        v-bind="activatorProps"
      >
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
</template>

<script>
export default {
  name: "NewFolderDialog",
  data: () => ({
    newFolderDialog: false,
    newFolderRules: [
      (value) => !!value || "У папки должно быть название",
      (value) =>
        (value && value.length >= 3) ||
        "Минимальная длинна названия - 3 символа",
    ],
  }),
  methods: {
  },
};
</script>
