import { Provider } from 'react-redux';
import './App.css';
import PermissionList from './components/PermissionList';
import UsersPanel from './components/UsersPanel';
import { store } from './store';

function App() {
  return (
    <Provider store={store}>
      <UsersPanel />
      <PermissionList />
    </Provider>
  );
}

export default App;
