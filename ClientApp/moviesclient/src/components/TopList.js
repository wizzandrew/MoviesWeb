import React, { Component } from 'react'
import axios from 'axios'

export default class TopList extends Component {
    state = {
        movies: [],
        newMovieId: 0
    }

    componentDidMount() {
        let id = this.props.match.params.topList_id;
        //console.log('toplist id:' + id);

        //get top list's info
        axios.get('https://localhost:44318/api/MovieList/1,' + id)
            .then(res => {
                //console.log(res);
                this.setState({
                    movies: res.data._movies
                })
            })
    }

    onIdChange = (e) => {
        this.setState({
            newMovieId: e.target.value
        })
    }

    onSubmit = (e) => {
        e.preventDefault();

        let movieIds = [];
        this.state.movies.map(movie =>{
            movieIds.push(parseInt(movie._id,10));
        })
        movieIds.push(parseInt(this.state.newMovieId,10));
        
        const topList = {
                "_id": parseInt(this.props.match.params.topList_id,10),
                "_title": null,
                "_userId": 1,
                "_movieIds": movieIds,
                "_movies": null
              }
        console.log(topList);

        let url = 'https://localhost:44318/api/MovieList?userId=1&listId=' + topList._id;
        axios.put(url , topList)
            .then(res => {
                console.log(res);
                this.setState({
                    newMovieId: null
                })
            })
    }

    onDelete = (e) => {
        axios.delete('https://localhost:44318/api/MovieList/1,' + this.props.match.params.topList_id)
            .then(res => {
                console.log(res);
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
                            <span className="card-title">{movie._title}</span>
                            <span>id: {movie._id}</span>
                            <span>  year: {movie._year}</span>
                            <span>  rating: {movie._rating}</span>
                        </div>
                    </div>
                )
            })
        ) : (
            <div className="center">No movies yet</div>
        )

        //real render
        return (
            <div className="container">
                <h4 className="center">Movies</h4>
                <div className="inputs">
                    <form onSubmit={this.onSubmit}>
                        <label>
                            Movie Id:
                            <input style={inputStyle} type="text" onChange={this.onIdChange} />
                        </label>
                        <button>Add Movie</button>
                    </form>
                </div>
                {movieList}
                <button onClick={this.onDelete}>Delete list</button>
            </div>
        )
    }
}
