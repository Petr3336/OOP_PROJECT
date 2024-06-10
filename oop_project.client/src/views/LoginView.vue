<template>
  <v-main class="d-flex justify-center align-center">
    <v-card
      class="mx-auto pa-12 pb-2 flex-grow-1"
      elevation="8"
      max-width="448"
      rounded="lg"
    >
      <div class="text-subtitle-1 text-medium-emphasis">Аккаунт</div>

      <v-text-field
        density="compact"
        placeholder="Почтовый адрес"
        prepend-inner-icon="mdi-email-outline"
        variant="outlined"
      ></v-text-field>

      <div
        class="text-subtitle-1 text-medium-emphasis d-flex align-center justify-space-between"
      >
        Пароль

        <a
          class="text-caption text-decoration-none text-blue"
          href="#"
          rel="noopener noreferrer"
          target="_blank"
        >
          Забыли пароль?</a
        >
      </div>

      <v-text-field
        :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
        :type="visible ? 'text' : 'password'"
        density="compact"
        placeholder="Введите ваш пароль"
        prepend-inner-icon="mdi-lock-outline"
        variant="outlined"
        @click:append-inner="visible = !visible"
      ></v-text-field>

      <!-- <v-card class="mb-12" color="surface-variant" variant="tonal">
            <v-card-text class="text-medium-emphasis text-caption">
              Warning: After 3 consecutive failed login attempts, you account
              will be temporarily locked for three hours. If you must login now,
              you can also click "Forgot login password?" below to reset the
              login password.
            </v-card-text>
          </v-card> -->

      <v-card class="mb-4" color="surface-variant" variant="tonal"> </v-card>

      <v-btn class="mb-2" color="blue" size="large" variant="tonal" block>
        Войти
      </v-btn>

      <v-card-text class="text-center">
        <RouterLink class="text-blue text-decoration-none" to="/register">
          Зарегистрироваться <v-icon icon="mdi-chevron-right"></v-icon>
        </RouterLink>
      </v-card-text>
    </v-card>
  </v-main>
</template>

<script>
export default {
  name: "LoginView",
  data: () => ({
    visible: false,
  }),
};
</script>
