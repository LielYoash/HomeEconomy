import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import AuthPage from './pages/AuthPage';
import ParentNotificationsPage from './components/ParentNotificationsPage';
import AddNotificationForm from './services/AddNotifications';
import ChildPage from './components/ChildPage';
import ParentTasksPage from "./components/ParentTasksPage";
import AddTaskPage from "./components/AddTaskPage"; // Import your diagram component

const App = () => {
  return (
      <Router>
        <div>
          <Switch>
            <Route exact path="/" component={AuthPage} />
            <Route path="/ParentNotificationsPage" component={ParentNotificationsPage} />
            <Route path="/ChildPage" component={ChildPage} />
            <Route path="/addNotifications" component={AddNotificationForm} />
            <Route path="/parentTasks" component={ParentTasksPage}/>
            <Route path="/addTask" component={AddTaskPage}/>
          </Switch>
        </div>
      </Router>
  );
};

export default App;


