import './App.css';
import { GetRedditReport } from './components/get-reddit-report.js';
import { InputSubredditComponent } from './components/input-subreddit.js';

function App() {
  return (
    <div>
      <header>
        <InputSubredditComponent />
        {/* <GetRedditReport /> */}
      </header>
    </div>
  );
}


export default App;
