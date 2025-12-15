
import { Links, Meta, Scripts, ScrollRestoration } from 'react-router';
import './root.css';
import Main from './components/main';



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
  return <Main />;
}

//console.log("Rendering App");
/*

console.log(document);
const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
console.log(root);
root.render(
  <BrowserRouter>
    <Provider store={store}>
      <SnackbarProvider
        autoHideDuration={3000} 
        anchorOrigin={{ horizontal: 'right', vertical: 'top' }}
        maxSnack={3}>
        <div>DocuEye App</div>
      </SnackbarProvider>
    </Provider>
  </BrowserRouter>
);*/
