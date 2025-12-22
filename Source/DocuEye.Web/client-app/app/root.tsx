
import { Links, Meta, Scripts, ScrollRestoration } from 'react-router';
import './root.css';
import Main from './components/main';
import { Provider } from 'react-redux';
import { SnackbarProvider } from 'notistack';
import store from './store';



export function Layout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <head>
        <meta charSet="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <Meta />
        <Links />
      </head>
      <body>
        {children}
        <ScrollRestoration />
        <Scripts />
      </body>
    </html>
  );
}

export default function App() {
  return (
    <Provider store={store}>
      <SnackbarProvider
        autoHideDuration={3000}
        anchorOrigin={{ horizontal: 'right', vertical: 'top' }}
        maxSnack={3}><Main /></SnackbarProvider>
    </Provider>
  );
}