import './App.css';
import { PresentString } from './components/present-string.js';
import { InputSubredditComponent } from './components/input-subreddit.js';


function App() {
  return (
    <div>
      <header>
        <InputSubredditComponent />
        {/* <PresentString /> */}
      </header>
    </div>
  );
}



export default App;
