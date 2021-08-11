import React, { Component } from 'react';
import Navbar from './components/Navbar'
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import Home from './components/Home'
import TopLists from './components/TopLists'
import TopMovies from './components/TopMovies'
import Movie from './components/Movie'
import TopList from './components/TopList'
// import MovieSearch from './components/MovieSearch';

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <div className="App">
          <Navbar />
          <Switch>
            <Route exact path='/' component={Home} />
            {/* <Route exact path='/movie' component={MovieSearch} /> */}
            <Route exact path='/toplists' component={TopLists} />
            <Route exact path='/topmovies' component={TopMovies} />
            <Route exact path='/:movie_id' component={Movie} />
            <Route exact path='/toplist/:topList_id' component={TopList} />
          </Switch>
        </div>
      </BrowserRouter>
    );
  }
}

export default App;
