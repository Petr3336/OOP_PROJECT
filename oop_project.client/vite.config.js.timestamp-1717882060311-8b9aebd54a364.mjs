// vite.config.js
import { fileURLToPath, URL } from "node:url";
import { defineConfig } from "file:///C:/Users/mist8/Documents/GitHub/OOP_PROJECT/oop_project.client/node_modules/vite/dist/node/index.js";
import vue from "file:///C:/Users/mist8/Documents/GitHub/OOP_PROJECT/oop_project.client/node_modules/@vitejs/plugin-vue/dist/index.mjs";
import fs from "fs";
import path from "path";
import child_process from "child_process";
import { env } from "process";
import { VitePWA } from "file:///C:/Users/mist8/Documents/GitHub/OOP_PROJECT/oop_project.client/node_modules/vite-plugin-pwa/dist/index.js";

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
import vuetify from "file:///C:/Users/mist8/Documents/GitHub/OOP_PROJECT/oop_project.client/node_modules/vite-plugin-vuetify/dist/index.mjs";
var __vite_injected_original_import_meta_url = "file:///C:/Users/mist8/Documents/GitHub/OOP_PROJECT/oop_project.client/vite.config.js";
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
        target: "https://localhost:5000",
        // ASP.NET Core API
        changeOrigin: true,
        secure: false
      },
      "/swagger": {
        target: "https://localhost:5000",
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
//# sourceMappingURL=data:application/json;base64,ewogICJ2ZXJzaW9uIjogMywKICAic291cmNlcyI6IFsidml0ZS5jb25maWcuanMiLCAibWFuaWZlc3QuanNvbiJdLAogICJzb3VyY2VzQ29udGVudCI6IFsiY29uc3QgX192aXRlX2luamVjdGVkX29yaWdpbmFsX2Rpcm5hbWUgPSBcIkM6XFxcXFVzZXJzXFxcXG1pc3Q4XFxcXERvY3VtZW50c1xcXFxHaXRIdWJcXFxcT09QX1BST0pFQ1RcXFxcb29wX3Byb2plY3QuY2xpZW50XCI7Y29uc3QgX192aXRlX2luamVjdGVkX29yaWdpbmFsX2ZpbGVuYW1lID0gXCJDOlxcXFxVc2Vyc1xcXFxtaXN0OFxcXFxEb2N1bWVudHNcXFxcR2l0SHViXFxcXE9PUF9QUk9KRUNUXFxcXG9vcF9wcm9qZWN0LmNsaWVudFxcXFx2aXRlLmNvbmZpZy5qc1wiO2NvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9pbXBvcnRfbWV0YV91cmwgPSBcImZpbGU6Ly8vQzovVXNlcnMvbWlzdDgvRG9jdW1lbnRzL0dpdEh1Yi9PT1BfUFJPSkVDVC9vb3BfcHJvamVjdC5jbGllbnQvdml0ZS5jb25maWcuanNcIjtpbXBvcnQgeyBmaWxlVVJMVG9QYXRoLCBVUkwgfSBmcm9tIFwibm9kZTp1cmxcIjtcclxuXHJcbmltcG9ydCB7IGRlZmluZUNvbmZpZyB9IGZyb20gXCJ2aXRlXCI7XHJcbmltcG9ydCB2dWUgZnJvbSBcIkB2aXRlanMvcGx1Z2luLXZ1ZVwiO1xyXG5pbXBvcnQgZnMgZnJvbSBcImZzXCI7XHJcbmltcG9ydCBwYXRoIGZyb20gXCJwYXRoXCI7XHJcbmltcG9ydCBjaGlsZF9wcm9jZXNzIGZyb20gXCJjaGlsZF9wcm9jZXNzXCI7XHJcbmltcG9ydCB7IGVudiB9IGZyb20gXCJwcm9jZXNzXCI7XHJcbmltcG9ydCB7IFZpdGVQV0EgfSBmcm9tIFwidml0ZS1wbHVnaW4tcHdhXCI7XHJcbmltcG9ydCBQV0FNYW5pZmVzdCBmcm9tIFwiLi9tYW5pZmVzdC5qc29uXCI7XHJcbmltcG9ydCB2dWV0aWZ5IGZyb20gJ3ZpdGUtcGx1Z2luLXZ1ZXRpZnknXHJcblxyXG5jb25zdCBiYXNlRm9sZGVyID1cclxuICBlbnYuQVBQREFUQSAhPT0gdW5kZWZpbmVkICYmIGVudi5BUFBEQVRBICE9PSBcIlwiXHJcbiAgICA/IGAke2Vudi5BUFBEQVRBfS9BU1AuTkVUL2h0dHBzYFxyXG4gICAgOiBgJHtlbnYuSE9NRX0vLmFzcG5ldC9odHRwc2A7XHJcblxyXG5jb25zdCBjZXJ0aWZpY2F0ZU5hbWUgPSBcIm9vcF9wcm9qZWN0LmNsaWVudFwiO1xyXG5jb25zdCBjZXJ0RmlsZVBhdGggPSBwYXRoLmpvaW4oYmFzZUZvbGRlciwgYCR7Y2VydGlmaWNhdGVOYW1lfS5wZW1gKTtcclxuY29uc3Qga2V5RmlsZVBhdGggPSBwYXRoLmpvaW4oYmFzZUZvbGRlciwgYCR7Y2VydGlmaWNhdGVOYW1lfS5rZXlgKTtcclxuXHJcbmlmICghZnMuZXhpc3RzU3luYyhjZXJ0RmlsZVBhdGgpIHx8ICFmcy5leGlzdHNTeW5jKGtleUZpbGVQYXRoKSkge1xyXG4gIGlmIChcclxuICAgIDAgIT09XHJcbiAgICBjaGlsZF9wcm9jZXNzLnNwYXduU3luYyhcclxuICAgICAgXCJkb3RuZXRcIixcclxuICAgICAgW1xyXG4gICAgICAgIFwiZGV2LWNlcnRzXCIsXHJcbiAgICAgICAgXCJodHRwc1wiLFxyXG4gICAgICAgIFwiLS1leHBvcnQtcGF0aFwiLFxyXG4gICAgICAgIGNlcnRGaWxlUGF0aCxcclxuICAgICAgICBcIi0tZm9ybWF0XCIsXHJcbiAgICAgICAgXCJQZW1cIixcclxuICAgICAgICBcIi0tbm8tcGFzc3dvcmRcIixcclxuICAgICAgXSxcclxuICAgICAgeyBzdGRpbzogXCJpbmhlcml0XCIgfVxyXG4gICAgKS5zdGF0dXNcclxuICApIHtcclxuICAgIHRocm93IG5ldyBFcnJvcihcIkNvdWxkIG5vdCBjcmVhdGUgY2VydGlmaWNhdGUuXCIpO1xyXG4gIH1cclxufVxyXG5cclxuaWYgKHByb2Nlc3MuZW52Lk5PREVfRU5WID09IFwiZGV2ZWxvcG1lbnRcIikge1xyXG4gIFBXQU1hbmlmZXN0Lm1hbmlmZXN0Lmljb25zWzBdLnNyYyA9IFwiL3NyYy9hc3NldHMvbG9nby5zdmdcIjtcclxufVxyXG5cclxuLy9jb25zdCB0YXJnZXQgPSBlbnYuQVNQTkVUQ09SRV9IVFRQU19QT1JUID8gYGh0dHBzOi8vbG9jYWxob3N0OiR7ZW52LkFTUE5FVENPUkVfSFRUUFNfUE9SVH1gIDpcclxuLy8gICAgZW52LkFTUE5FVENPUkVfVVJMUyA/IGVudi5BU1BORVRDT1JFX1VSTFMuc3BsaXQoJzsnKVswXSA6ICdodHRwczovL2xvY2FsaG9zdDo3MTM5JztcclxuXHJcbi8vIGh0dHBzOi8vdml0ZWpzLmRldi9jb25maWcvXHJcbmV4cG9ydCBkZWZhdWx0IGRlZmluZUNvbmZpZyh7XHJcbiAgcGx1Z2luczogW1xyXG4gICAgdnVlKCksXHJcbiAgICB2dWV0aWZ5KHsgYXV0b0ltcG9ydDogdHJ1ZSB9KSwgLy8gRW5hYmxlZCBieSBkZWZhdWx0XHJcbiAgICBWaXRlUFdBKHtcclxuICAgICAgcmVnaXN0ZXJUeXBlOiBcImF1dG9VcGRhdGVcIixcclxuICAgICAgZGV2T3B0aW9uczoge1xyXG4gICAgICAgIGVuYWJsZWQ6IHRydWUsXHJcbiAgICAgIH0sXHJcbiAgICAgIG1hbmlmZXN0OiBQV0FNYW5pZmVzdC5tYW5pZmVzdCxcclxuICAgIH0pLFxyXG4gIF0sXHJcbiAgY3NzQ29kZVNwbGl0OiB0cnVlLFxyXG4gIHJvbGx1cE9wdGlvbnM6IHtcclxuICAgIG91dHB1dDoge1xyXG4gICAgICAvLyBEZWZpbmUgbWFudWFsIGNodW5rcyBmb3IgZGVwZW5kZW5jaWVzXHJcbiAgICAgIG1hbnVhbENodW5rcyhpZCkge1xyXG4gICAgICAgIGlmIChpZC5pbmNsdWRlcyhcIm5vZGVfbW9kdWxlc1wiKSkge1xyXG4gICAgICAgICAgcmV0dXJuIFwidmVuZG9yXCI7XHJcbiAgICAgICAgfVxyXG4gICAgICB9LFxyXG4gICAgfSxcclxuICB9LFxyXG4gIHJlc29sdmU6IHtcclxuICAgIGFsaWFzOiB7XHJcbiAgICAgIFwiQFwiOiBmaWxlVVJMVG9QYXRoKG5ldyBVUkwoXCIuL3NyY1wiLCBpbXBvcnQubWV0YS51cmwpKSxcclxuICAgIH0sXHJcbiAgfSxcclxuICBzZXJ2ZXI6IHtcclxuICAgIHByb3h5OiB7XHJcbiAgICAgIFwiL2FwaVwiOiB7XHJcbiAgICAgICAgdGFyZ2V0OiBcImh0dHBzOi8vbG9jYWxob3N0OjUwMDBcIiwgLy8gQVNQLk5FVCBDb3JlIEFQSVxyXG4gICAgICAgIGNoYW5nZU9yaWdpbjogdHJ1ZSxcclxuICAgICAgICBzZWN1cmU6IGZhbHNlLFxyXG4gICAgICB9LFxyXG4gICAgICBcIi9zd2FnZ2VyXCI6IHtcclxuICAgICAgICB0YXJnZXQ6IFwiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMFwiLCAvLyBTd2FnZ2VyIGFwaSByZW5kZXJlclxyXG4gICAgICAgIGNoYW5nZU9yaWdpbjogdHJ1ZSxcclxuICAgICAgICBzZWN1cmU6IGZhbHNlLFxyXG4gICAgICB9LFxyXG4gICAgfSxcclxuICAgIHBvcnQ6IHBhcnNlSW50KHByb2Nlc3MuZW52LlBPUlQgPz8gXCI4MFwiKSxcclxuICAgIGh0dHBzOiB7XHJcbiAgICAgIGtleTogZnMucmVhZEZpbGVTeW5jKGtleUZpbGVQYXRoKSxcclxuICAgICAgY2VydDogZnMucmVhZEZpbGVTeW5jKGNlcnRGaWxlUGF0aCksXHJcbiAgICB9LFxyXG4gIH0sXHJcbn0pO1xyXG4iLCAie1xyXG4gIFwibWFuaWZlc3RcIjoge1xyXG4gICAgXCJuYW1lXCI6IFwiTXkgQXdlc29tZSBBcHBcIixcclxuICAgIFwic2hvcnRfbmFtZVwiOiBcIkF3ZXNvbWVBcHBcIixcclxuICAgIFwiZGVzY3JpcHRpb25cIjogXCJNeSBBd2Vzb21lIEFwcCBkZXNjcmlwdGlvblwiLFxyXG4gICAgXCJ0aGVtZV9jb2xvclwiOiBcIiM2NmNjZmZcIixcclxuICAgIFwiaWNvbnNcIjogW1xyXG4gICAgICB7XHJcbiAgICAgICAgXCJzcmNcIjogXCIvbG9nby5zdmdcIixcclxuICAgICAgICBcInNpemVzXCI6IFwiYW55XCIsXHJcbiAgICAgICAgXCJ0eXBlXCI6IFwiaW1hZ2Uvc3ZnK3htbFwiXHJcbiAgICAgIH1cclxuICAgIF1cclxuICB9XHJcbn1cclxuIl0sCiAgIm1hcHBpbmdzIjogIjtBQUEwWCxTQUFTLGVBQWUsV0FBVztBQUU3WixTQUFTLG9CQUFvQjtBQUM3QixPQUFPLFNBQVM7QUFDaEIsT0FBTyxRQUFRO0FBQ2YsT0FBTyxVQUFVO0FBQ2pCLE9BQU8sbUJBQW1CO0FBQzFCLFNBQVMsV0FBVztBQUNwQixTQUFTLGVBQWU7OztBQ1J4QjtBQUFBLEVBQ0UsVUFBWTtBQUFBLElBQ1YsTUFBUTtBQUFBLElBQ1IsWUFBYztBQUFBLElBQ2QsYUFBZTtBQUFBLElBQ2YsYUFBZTtBQUFBLElBQ2YsT0FBUztBQUFBLE1BQ1A7QUFBQSxRQUNFLEtBQU87QUFBQSxRQUNQLE9BQVM7QUFBQSxRQUNULE1BQVE7QUFBQSxNQUNWO0FBQUEsSUFDRjtBQUFBLEVBQ0Y7QUFDRjs7O0FESkEsT0FBTyxhQUFhO0FBVjZOLElBQU0sMkNBQTJDO0FBWWxTLElBQU0sYUFDSixJQUFJLFlBQVksVUFBYSxJQUFJLFlBQVksS0FDekMsR0FBRyxJQUFJLE9BQU8sbUJBQ2QsR0FBRyxJQUFJLElBQUk7QUFFakIsSUFBTSxrQkFBa0I7QUFDeEIsSUFBTSxlQUFlLEtBQUssS0FBSyxZQUFZLEdBQUcsZUFBZSxNQUFNO0FBQ25FLElBQU0sY0FBYyxLQUFLLEtBQUssWUFBWSxHQUFHLGVBQWUsTUFBTTtBQUVsRSxJQUFJLENBQUMsR0FBRyxXQUFXLFlBQVksS0FBSyxDQUFDLEdBQUcsV0FBVyxXQUFXLEdBQUc7QUFDL0QsTUFDRSxNQUNBLGNBQWM7QUFBQSxJQUNaO0FBQUEsSUFDQTtBQUFBLE1BQ0U7QUFBQSxNQUNBO0FBQUEsTUFDQTtBQUFBLE1BQ0E7QUFBQSxNQUNBO0FBQUEsTUFDQTtBQUFBLE1BQ0E7QUFBQSxJQUNGO0FBQUEsSUFDQSxFQUFFLE9BQU8sVUFBVTtBQUFBLEVBQ3JCLEVBQUUsUUFDRjtBQUNBLFVBQU0sSUFBSSxNQUFNLCtCQUErQjtBQUFBLEVBQ2pEO0FBQ0Y7QUFFQSxJQUFJLFFBQVEsSUFBSSxZQUFZLGVBQWU7QUFDekMsbUJBQVksU0FBUyxNQUFNLENBQUMsRUFBRSxNQUFNO0FBQ3RDO0FBTUEsSUFBTyxzQkFBUSxhQUFhO0FBQUEsRUFDMUIsU0FBUztBQUFBLElBQ1AsSUFBSTtBQUFBLElBQ0osUUFBUSxFQUFFLFlBQVksS0FBSyxDQUFDO0FBQUE7QUFBQSxJQUM1QixRQUFRO0FBQUEsTUFDTixjQUFjO0FBQUEsTUFDZCxZQUFZO0FBQUEsUUFDVixTQUFTO0FBQUEsTUFDWDtBQUFBLE1BQ0EsVUFBVSxpQkFBWTtBQUFBLElBQ3hCLENBQUM7QUFBQSxFQUNIO0FBQUEsRUFDQSxjQUFjO0FBQUEsRUFDZCxlQUFlO0FBQUEsSUFDYixRQUFRO0FBQUE7QUFBQSxNQUVOLGFBQWEsSUFBSTtBQUNmLFlBQUksR0FBRyxTQUFTLGNBQWMsR0FBRztBQUMvQixpQkFBTztBQUFBLFFBQ1Q7QUFBQSxNQUNGO0FBQUEsSUFDRjtBQUFBLEVBQ0Y7QUFBQSxFQUNBLFNBQVM7QUFBQSxJQUNQLE9BQU87QUFBQSxNQUNMLEtBQUssY0FBYyxJQUFJLElBQUksU0FBUyx3Q0FBZSxDQUFDO0FBQUEsSUFDdEQ7QUFBQSxFQUNGO0FBQUEsRUFDQSxRQUFRO0FBQUEsSUFDTixPQUFPO0FBQUEsTUFDTCxRQUFRO0FBQUEsUUFDTixRQUFRO0FBQUE7QUFBQSxRQUNSLGNBQWM7QUFBQSxRQUNkLFFBQVE7QUFBQSxNQUNWO0FBQUEsTUFDQSxZQUFZO0FBQUEsUUFDVixRQUFRO0FBQUE7QUFBQSxRQUNSLGNBQWM7QUFBQSxRQUNkLFFBQVE7QUFBQSxNQUNWO0FBQUEsSUFDRjtBQUFBLElBQ0EsTUFBTSxTQUFTLFFBQVEsSUFBSSxRQUFRLElBQUk7QUFBQSxJQUN2QyxPQUFPO0FBQUEsTUFDTCxLQUFLLEdBQUcsYUFBYSxXQUFXO0FBQUEsTUFDaEMsTUFBTSxHQUFHLGFBQWEsWUFBWTtBQUFBLElBQ3BDO0FBQUEsRUFDRjtBQUNGLENBQUM7IiwKICAibmFtZXMiOiBbXQp9Cg==
