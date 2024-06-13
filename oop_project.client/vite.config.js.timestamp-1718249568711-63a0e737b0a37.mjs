// vite.config.js
import { fileURLToPath, URL } from "node:url";
import { defineConfig } from "file:///C:/Users/petrp/Documents/OOP_PROJECT/oop_project.client/node_modules/vite/dist/node/index.js";
import vue from "file:///C:/Users/petrp/Documents/OOP_PROJECT/oop_project.client/node_modules/@vitejs/plugin-vue/dist/index.mjs";
import fs from "fs";
import path from "path";
import child_process from "child_process";
import { env } from "process";
import { VitePWA } from "file:///C:/Users/petrp/Documents/OOP_PROJECT/oop_project.client/node_modules/vite-plugin-pwa/dist/index.js";

// manifest.json
var manifest_default = {
  manifest: {
    name: "My Awesome App",
    short_name: "AwesomeApp",
    description: "My Awesome App description",
    theme_color: "#66ccff",
    icons: [
      {
        src: "/logo.svg",
        sizes: "any",
        type: "image/svg+xml"
      }
    ]
  }
};

// vite.config.js
import vuetify from "file:///C:/Users/petrp/Documents/OOP_PROJECT/oop_project.client/node_modules/vite-plugin-vuetify/dist/index.mjs";
var __vite_injected_original_import_meta_url = "file:///C:/Users/petrp/Documents/OOP_PROJECT/oop_project.client/vite.config.js";
var baseFolder = env.APPDATA !== void 0 && env.APPDATA !== "" ? `${env.APPDATA}/ASP.NET/https` : `${env.HOME}/.aspnet/https`;
var certificateName = "oop_project.client";
var certFilePath = path.join(baseFolder, `${certificateName}.pem`);
var keyFilePath = path.join(baseFolder, `${certificateName}.key`);
if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
  if (0 !== child_process.spawnSync(
    "dotnet",
    [
      "dev-certs",
      "https",
      "--export-path",
      certFilePath,
      "--format",
      "Pem",
      "--no-password"
    ],
    { stdio: "inherit" }
  ).status) {
    throw new Error("Could not create certificate.");
  }
}
if (process.env.NODE_ENV == "development") {
  manifest_default.manifest.icons[0].src = "/src/assets/logo.svg";
}
var vite_config_default = defineConfig({
  plugins: [
    vue(),
    vuetify({ autoImport: true }),
    // Enabled by default
    VitePWA({
      registerType: "autoUpdate",
      devOptions: {
        enabled: true
      },
      manifest: manifest_default.manifest
    })
  ],
  cssCodeSplit: true,
  rollupOptions: {
    output: {
      // Define manual chunks for dependencies
      manualChunks(id) {
        if (id.includes("node_modules")) {
          return "vendor";
        }
      }
    }
  },
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", __vite_injected_original_import_meta_url))
    }
  },
  server: {
    proxy: {
      "/api": {
        target: "https://localhost:7076",
        // ASP.NET Core API
        changeOrigin: true,
        secure: false
      },
      "/swagger": {
        target: "https://localhost:7076",
        // Swagger api renderer
        changeOrigin: true,
        secure: false
      }
    },
    port: parseInt(process.env.PORT ?? "80"),
    https: {
      key: fs.readFileSync(keyFilePath),
      cert: fs.readFileSync(certFilePath)
    }
  }
});
export {
  vite_config_default as default
};
//# sourceMappingURL=data:application/json;base64,ewogICJ2ZXJzaW9uIjogMywKICAic291cmNlcyI6IFsidml0ZS5jb25maWcuanMiLCAibWFuaWZlc3QuanNvbiJdLAogICJzb3VyY2VzQ29udGVudCI6IFsiY29uc3QgX192aXRlX2luamVjdGVkX29yaWdpbmFsX2Rpcm5hbWUgPSBcIkM6XFxcXFVzZXJzXFxcXHBldHJwXFxcXERvY3VtZW50c1xcXFxPT1BfUFJPSkVDVFxcXFxvb3BfcHJvamVjdC5jbGllbnRcIjtjb25zdCBfX3ZpdGVfaW5qZWN0ZWRfb3JpZ2luYWxfZmlsZW5hbWUgPSBcIkM6XFxcXFVzZXJzXFxcXHBldHJwXFxcXERvY3VtZW50c1xcXFxPT1BfUFJPSkVDVFxcXFxvb3BfcHJvamVjdC5jbGllbnRcXFxcdml0ZS5jb25maWcuanNcIjtjb25zdCBfX3ZpdGVfaW5qZWN0ZWRfb3JpZ2luYWxfaW1wb3J0X21ldGFfdXJsID0gXCJmaWxlOi8vL0M6L1VzZXJzL3BldHJwL0RvY3VtZW50cy9PT1BfUFJPSkVDVC9vb3BfcHJvamVjdC5jbGllbnQvdml0ZS5jb25maWcuanNcIjtpbXBvcnQgeyBmaWxlVVJMVG9QYXRoLCBVUkwgfSBmcm9tIFwibm9kZTp1cmxcIjtcblxuaW1wb3J0IHsgZGVmaW5lQ29uZmlnIH0gZnJvbSBcInZpdGVcIjtcbmltcG9ydCB2dWUgZnJvbSBcIkB2aXRlanMvcGx1Z2luLXZ1ZVwiO1xuaW1wb3J0IGZzIGZyb20gXCJmc1wiO1xuaW1wb3J0IHBhdGggZnJvbSBcInBhdGhcIjtcbmltcG9ydCBjaGlsZF9wcm9jZXNzIGZyb20gXCJjaGlsZF9wcm9jZXNzXCI7XG5pbXBvcnQgeyBlbnYgfSBmcm9tIFwicHJvY2Vzc1wiO1xuaW1wb3J0IHsgVml0ZVBXQSB9IGZyb20gXCJ2aXRlLXBsdWdpbi1wd2FcIjtcbmltcG9ydCBQV0FNYW5pZmVzdCBmcm9tIFwiLi9tYW5pZmVzdC5qc29uXCI7XG5pbXBvcnQgdnVldGlmeSBmcm9tICd2aXRlLXBsdWdpbi12dWV0aWZ5J1xuXG5jb25zdCBiYXNlRm9sZGVyID1cbiAgZW52LkFQUERBVEEgIT09IHVuZGVmaW5lZCAmJiBlbnYuQVBQREFUQSAhPT0gXCJcIlxuICAgID8gYCR7ZW52LkFQUERBVEF9L0FTUC5ORVQvaHR0cHNgXG4gICAgOiBgJHtlbnYuSE9NRX0vLmFzcG5ldC9odHRwc2A7XG5cbmNvbnN0IGNlcnRpZmljYXRlTmFtZSA9IFwib29wX3Byb2plY3QuY2xpZW50XCI7XG5jb25zdCBjZXJ0RmlsZVBhdGggPSBwYXRoLmpvaW4oYmFzZUZvbGRlciwgYCR7Y2VydGlmaWNhdGVOYW1lfS5wZW1gKTtcbmNvbnN0IGtleUZpbGVQYXRoID0gcGF0aC5qb2luKGJhc2VGb2xkZXIsIGAke2NlcnRpZmljYXRlTmFtZX0ua2V5YCk7XG5cbmlmICghZnMuZXhpc3RzU3luYyhjZXJ0RmlsZVBhdGgpIHx8ICFmcy5leGlzdHNTeW5jKGtleUZpbGVQYXRoKSkge1xuICBpZiAoXG4gICAgMCAhPT1cbiAgICBjaGlsZF9wcm9jZXNzLnNwYXduU3luYyhcbiAgICAgIFwiZG90bmV0XCIsXG4gICAgICBbXG4gICAgICAgIFwiZGV2LWNlcnRzXCIsXG4gICAgICAgIFwiaHR0cHNcIixcbiAgICAgICAgXCItLWV4cG9ydC1wYXRoXCIsXG4gICAgICAgIGNlcnRGaWxlUGF0aCxcbiAgICAgICAgXCItLWZvcm1hdFwiLFxuICAgICAgICBcIlBlbVwiLFxuICAgICAgICBcIi0tbm8tcGFzc3dvcmRcIixcbiAgICAgIF0sXG4gICAgICB7IHN0ZGlvOiBcImluaGVyaXRcIiB9XG4gICAgKS5zdGF0dXNcbiAgKSB7XG4gICAgdGhyb3cgbmV3IEVycm9yKFwiQ291bGQgbm90IGNyZWF0ZSBjZXJ0aWZpY2F0ZS5cIik7XG4gIH1cbn1cblxuaWYgKHByb2Nlc3MuZW52Lk5PREVfRU5WID09IFwiZGV2ZWxvcG1lbnRcIikge1xuICBQV0FNYW5pZmVzdC5tYW5pZmVzdC5pY29uc1swXS5zcmMgPSBcIi9zcmMvYXNzZXRzL2xvZ28uc3ZnXCI7XG59XG5cbi8vY29uc3QgdGFyZ2V0ID0gZW52LkFTUE5FVENPUkVfSFRUUFNfUE9SVCA/IGBodHRwczovL2xvY2FsaG9zdDoke2Vudi5BU1BORVRDT1JFX0hUVFBTX1BPUlR9YCA6XG4vLyAgICBlbnYuQVNQTkVUQ09SRV9VUkxTID8gZW52LkFTUE5FVENPUkVfVVJMUy5zcGxpdCgnOycpWzBdIDogJ2h0dHBzOi8vbG9jYWxob3N0OjcxMzknO1xuXG4vLyBodHRwczovL3ZpdGVqcy5kZXYvY29uZmlnL1xuZXhwb3J0IGRlZmF1bHQgZGVmaW5lQ29uZmlnKHtcbiAgcGx1Z2luczogW1xuICAgIHZ1ZSgpLFxuICAgIHZ1ZXRpZnkoeyBhdXRvSW1wb3J0OiB0cnVlIH0pLCAvLyBFbmFibGVkIGJ5IGRlZmF1bHRcbiAgICBWaXRlUFdBKHtcbiAgICAgIHJlZ2lzdGVyVHlwZTogXCJhdXRvVXBkYXRlXCIsXG4gICAgICBkZXZPcHRpb25zOiB7XG4gICAgICAgIGVuYWJsZWQ6IHRydWUsXG4gICAgICB9LFxuICAgICAgbWFuaWZlc3Q6IFBXQU1hbmlmZXN0Lm1hbmlmZXN0LFxuICAgIH0pLFxuICBdLFxuICBjc3NDb2RlU3BsaXQ6IHRydWUsXG4gIHJvbGx1cE9wdGlvbnM6IHtcbiAgICBvdXRwdXQ6IHtcbiAgICAgIC8vIERlZmluZSBtYW51YWwgY2h1bmtzIGZvciBkZXBlbmRlbmNpZXNcbiAgICAgIG1hbnVhbENodW5rcyhpZCkge1xuICAgICAgICBpZiAoaWQuaW5jbHVkZXMoXCJub2RlX21vZHVsZXNcIikpIHtcbiAgICAgICAgICByZXR1cm4gXCJ2ZW5kb3JcIjtcbiAgICAgICAgfVxuICAgICAgfSxcbiAgICB9LFxuICB9LFxuICByZXNvbHZlOiB7XG4gICAgYWxpYXM6IHtcbiAgICAgIFwiQFwiOiBmaWxlVVJMVG9QYXRoKG5ldyBVUkwoXCIuL3NyY1wiLCBpbXBvcnQubWV0YS51cmwpKSxcbiAgICB9LFxuICB9LFxuICBzZXJ2ZXI6IHtcbiAgICBwcm94eToge1xuICAgICAgXCIvYXBpXCI6IHtcbiAgICAgICAgdGFyZ2V0OiBcImh0dHBzOi8vbG9jYWxob3N0OjcwNzZcIiwgLy8gQVNQLk5FVCBDb3JlIEFQSVxuICAgICAgICBjaGFuZ2VPcmlnaW46IHRydWUsXG4gICAgICAgIHNlY3VyZTogZmFsc2UsXG4gICAgICB9LFxuICAgICAgXCIvc3dhZ2dlclwiOiB7XG4gICAgICAgIHRhcmdldDogXCJodHRwczovL2xvY2FsaG9zdDo3MDc2XCIsIC8vIFN3YWdnZXIgYXBpIHJlbmRlcmVyXG4gICAgICAgIGNoYW5nZU9yaWdpbjogdHJ1ZSxcbiAgICAgICAgc2VjdXJlOiBmYWxzZSxcbiAgICAgIH0sXG4gICAgfSxcbiAgICBwb3J0OiBwYXJzZUludChwcm9jZXNzLmVudi5QT1JUID8/IFwiODBcIiksXG4gICAgaHR0cHM6IHtcbiAgICAgIGtleTogZnMucmVhZEZpbGVTeW5jKGtleUZpbGVQYXRoKSxcbiAgICAgIGNlcnQ6IGZzLnJlYWRGaWxlU3luYyhjZXJ0RmlsZVBhdGgpLFxuICAgIH0sXG4gIH0sXG59KTtcbiIsICJ7XG4gIFwibWFuaWZlc3RcIjoge1xuICAgIFwibmFtZVwiOiBcIk15IEF3ZXNvbWUgQXBwXCIsXG4gICAgXCJzaG9ydF9uYW1lXCI6IFwiQXdlc29tZUFwcFwiLFxuICAgIFwiZGVzY3JpcHRpb25cIjogXCJNeSBBd2Vzb21lIEFwcCBkZXNjcmlwdGlvblwiLFxuICAgIFwidGhlbWVfY29sb3JcIjogXCIjNjZjY2ZmXCIsXG4gICAgXCJpY29uc1wiOiBbXG4gICAgICB7XG4gICAgICAgIFwic3JjXCI6IFwiL2xvZ28uc3ZnXCIsXG4gICAgICAgIFwic2l6ZXNcIjogXCJhbnlcIixcbiAgICAgICAgXCJ0eXBlXCI6IFwiaW1hZ2Uvc3ZnK3htbFwiXG4gICAgICB9XG4gICAgXVxuICB9XG59XG4iXSwKICAibWFwcGluZ3MiOiAiO0FBQW1XLFNBQVMsZUFBZSxXQUFXO0FBRXRZLFNBQVMsb0JBQW9CO0FBQzdCLE9BQU8sU0FBUztBQUNoQixPQUFPLFFBQVE7QUFDZixPQUFPLFVBQVU7QUFDakIsT0FBTyxtQkFBbUI7QUFDMUIsU0FBUyxXQUFX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
