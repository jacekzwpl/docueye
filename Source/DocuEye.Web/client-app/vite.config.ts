import { reactRouter } from "@react-router/dev/vite";
import tailwindcss from "@tailwindcss/vite";
import { defineConfig, loadEnv } from "vite";
import tsconfigPaths from "vite-tsconfig-paths";
import fs from "fs";


Object.assign(process.env, loadEnv(process.env.NODE_ENV ?? "development", process.cwd(), ""));

const options = process.env.NODE_ENV === "development" ? {
  key: fs.readFileSync(process.env.SSL_KEY_FILE ?? ""),
  cert: fs.readFileSync(process.env.SSL_CRT_FILE ?? ""),
} : {};

export default defineConfig({
  server: {
    https: options,
    port: 3000,
    proxy: {
      "/api": {
        target: "https://localhost:7041",
        changeOrigin: true,
        secure: false,
      },
    }
  },
  plugins: [tailwindcss(), reactRouter(), tsconfigPaths()],
  resolve: {
    alias: {
      util: "util/",
      process: "process/browser"
    },
  },
  define: {
    "process.env": {},
  },
  optimizeDeps: {
    include: ["util", "process"] //"buffer"
  },
});
