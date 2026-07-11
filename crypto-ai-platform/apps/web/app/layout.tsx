import type { Metadata } from "next";
import "./globals.css";
import { Providers } from "./providers/providers";
import { ThemeProvider } from "./providers/theme-provider";

export const metadata: Metadata = {
  title: "Crypto AI Platform",
  description: "Plataforma enterprise de negociação quantitativa de criptomoedas",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="pt-BR" suppressHydrationWarning>
      <body>
        <ThemeProvider defaultTheme="dark" storageKey="crypto-ai-theme">
          <Providers>
            {children}
          </Providers>
        </ThemeProvider>
      </body>
    </html>
  );
}
