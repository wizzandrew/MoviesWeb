import React, { Component } from 'react';
import axios from 'axios'
import { Link } from 'react-router-dom'

class TopMovies extends Component {
    state = {
        movies: [],
        avgRating: null,
        from: null,
        to: null
    }

    componentDidMount() {
        axios.get('https://localhost:44318/api/Movies/top10')
            .then(res => {
                // console.log(res);
                this.setState({
                    movies: res.data
                })
            })
    }

    onFromChange = (e) => {
        this.setState({
            from: e.target.value
        })
    }

    onToChange = (e) => {
        this.setState({
            to: e.target.value
        })
    }

    onSubmit = (e) => {
        e.preventDefault();
        //console.log('form submitted', this.state.from, this.state.to);
        axios.get('https://localhost:44318/api/Movies/average/' + this.state.from + ',' + this.state.to)
            .then(res => {
                console.log(res);
                this.setState({
                    avgRating: res.data
                })
            })
    }

    render() {
        //input style
        const inputStyle = {
            width: "50px",
            marginRight: "15px",
            height: "22px"
        };

        const { movies } = this.state;
        const movieList = movies.length ? (
            movies.map(movie => {
                return (
                    <div className="movie card" key={movie._id}>
                        <div className="card-content">
                            <Link to={'/' + movie._id} >
                                <span className="card-title">{movie._title}</span>
                            </Link>
                            <span>  year: {movie._year}</span>
                            <span>  rating: {movie._rating}</span>
                        </div>
                    </div>
                )
            })
        ) : (
            <div className="center">No top movies yet</div>
        )

        return (
            <div className="container">
                <div className="avgRatingMovies">
                    <h4 className="center">Average movie rating</h4>
                    <div className="inputs">
                        <form onSubmit={this.onSubmit}>
                            <input style={inputStyle} type="text" onChange={this.onFromChange} />
                            <input style={inputStyle} type="text" onChange={this.onToChange} />
                            <button>Calculate</button>
                        </form>
                    </div>
                    <span>Average rating of movies (1970-2020): <b>{this.state.avgRating}</b></span>
                </div>
                <div className="topTenMovies">
                    <h4 className="center">Top 10 Movies</h4>
                    {movieList}
                </div>
            </div>
        )
    }
}

export default TopMovies