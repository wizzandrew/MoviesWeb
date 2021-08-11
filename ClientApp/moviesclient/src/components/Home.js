import React, { Component } from 'react';
import axios from 'axios'
import { Link } from 'react-router-dom'
import SearchField from 'react-search-field'

class Home extends Component {
    state = {
        movies: [],
        searchMovie: null
    }

    componentDidMount() {
        axios.get('https://localhost:44318/api/movies')
            .then(res => {
                // console.log(res);
                this.setState({
                    movies: res.data
                })
            })
    }

    movieSearch = (title) => {
        axios.get('https://localhost:44318/api/Movies/title/' + title)
            .then(res => {
                console.log(res.data[0]._id);
                this.setState({
                    searchMovie: res.data[0]
                })
            })
    }

    render() {

        //list of movies
        const { movies } = this.state;
        const movieList = movies.length ? (
            movies.map(movie => {
                return (
                    <div className="movie card" key={movie._id}>
                        <div className="card-content">
                            <Link to={'/' + movie._id} >
                                <span className="card-title">{movie._title}</span>
                            </Link>
                            <span>  id: {movie._id}</span>
                            <span>  year: {movie._year}</span>
                            <span>  rating: {movie._rating}</span>
                        </div>
                    </div>
                )
            })
        ) : (
            <div className="center">No movies yet</div>
        )

        //search for a movie
        const searchMovie = this.state.searchMovie ? (
            <div className="movie card" key={this.state.searchMovie._id}>
                <div className="card-content">
                    <Link to={'/' + this.state.searchMovie._id} >
                        <span className="card-title">{this.state.searchMovie._title}</span>
                    </Link>
                    <span>  id: {this.state.searchMovie._id}</span>
                    <span>  year: {this.state.searchMovie._year}</span>
                    <span>  rating: {this.state.searchMovie._rating}</span>
                </div>
            </div>
        )

            : (
                <div className="center">No searched movie yet</div>
            )



        //real render
        return (
            <div className="container">
                <div className="subContainer">
                    <h4 className="center">Movie Search</h4>
                    <div className="movieSearch">
                        <SearchField
                            placeholder="movie's title"
                            onSearchClick={this.movieSearch}
                        />
                    </div>
                    {searchMovie}
                </div> <br />
                <h4 className="center">Movies</h4>
                {movieList}
            </div>
        )
    }
}

export default Home