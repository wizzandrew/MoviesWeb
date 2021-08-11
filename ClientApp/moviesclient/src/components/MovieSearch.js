import React, { Component } from 'react'
import axios from 'axios'
import SearchField from 'react-search-field'
import Movie from './Movie'

class MovieSearch extends Component {
    state = {
        movie: []
    }

    movieSearch = (title) => {
        axios.get('https://movieswebsep6.azurewebsites.net/api/Movies/title/' + title)
            .then(res => {
                console.log(res);
                this.setState({
                    movie: res.data
                })
            })

    }

    render() {
        //movie info
        const { movie } = this.state;
        const moviees = movie.length ? (
            moviees.map(moviee => {
                return (
                    <div className="movie" key={moviee._id}>
                        <Movie movie_id={moviee._id} />
                    </div>
                )
            })
        )
            :
            (
                <div className="center">Loading movie...</div>
            )

        return (
            <div className="container">
                <h4 className="center">Movie Search</h4>
                <div className="movieSearch">
                    <SearchField
                        placeholder="movie's title"
                        onSearchClick={this.movieSearch}
                    />
                </div>
                {moviees}
            </div>
        )
    }
}

export default MovieSearch