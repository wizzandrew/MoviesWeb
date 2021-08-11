import React, { Component } from 'react'
import axios from 'axios'
import SearchField from 'react-search-field'

class Movie extends Component {
    state = {
        movie: null,
        directors: [],
        actors: [],
        directorRating: null,
        actorRating: null
    }

    componentDidMount() {
        let id = this.props.match.params.movie_id;

        //get movie info
        axios.get('https://movieswebsep6.azurewebsites.net/api/Movies/' + id)
            .then(res => {
                console.log(res);
                this.setState({
                    movie: res.data
                })
            })

        //get director info
        axios.get('https://movieswebsep6.azurewebsites.net/api/Director/' + id)
            .then(res => {
                console.log(res);
                this.setState({
                    directors: res.data
                })
            })

        //get actor info
        axios.get('https://movieswebsep6.azurewebsites.net/api/Actor/' + id)
            .then(res => {
                console.log(res);
                this.setState({
                    actors: res.data
                })
            })
    }

    directorSearch = (id) => {
        axios.get('https://movieswebsep6.azurewebsites.net/api/Director/rating/' + id)
            .then(res => {
                console.log(res);
                this.setState({
                    directorRating: res.data
                })
            })
    }

    actorSearch = (id) => {
        axios.get('https://movieswebsep6.azurewebsites.net/api/Actor/rating/' + id)
            .then(res => {
                console.log(res);
                this.setState({
                    actorRating: res.data
                })
            })
    }

    render() {

        //movie info
        const movie = this.state.movie ? (
            <div className="movie">
                <h6>title: {this.state.movie._title}</h6>
                <span>id: {this.state.movie._id}, </span>
                <span>year: {this.state.movie._year}, </span>
                <span>rating: {this.state.movie._rating}</span>
            </div>
        )

            :

            (
                <div className="center">Loading movie...</div>
            )

        //directors
        const { directors } = this.state;
        const directorList = directors.length ? (
            directors.map(director => {
                return (
                    <div className="director" key={director._id}>
                        <span>(id: {director._id}) </span>
                        <span>{director._name}, </span>
                        <span>{director._birth}</span>
                    </div>
                )
            })
        ) : (
            <div className="director">No directors</div>
        )

        //actors
        const { actors } = this.state;
        const actorList = actors.length ? (
            actors.map(actor => {
                return (
                    <div className="actor" key={actor._id}>
                        <span>(id: {actor._id}) </span>
                        <span>{actor._name}, </span>
                        <span>{actor._birth}</span>
                    </div>
                )
            })
        ) : (
            <div className="actor">No actors</div>
        )

        return (
            <div className="container">
                <div className="movieCard">
                    <h5>Basic info</h5> {movie} <br />
                    <h5>Directors:</h5> {directorList} <br />
                    <h5>Actors:</h5> {actorList} <br />
                </div>

                <div className="movieStats">
                    <div className="directorStat">
                        <h5>Statistics for director</h5>
                        <SearchField
                            placeholder="director's id"
                            onSearchClick={this.directorSearch}
                        />
                        <span>  Director's avg rating of all movies filmed: <b>{this.state.directorRating}</b></span>
                    </div>

                    <br />

                    <div className="actorStat">
                        <h5>Statistics for actor</h5>
                        <SearchField
                            placeholder="actor's id"
                            onSearchClick={this.actorSearch}
                        />
                        <span>  Actor's avg rating of all movies starred in: <b>{this.state.actorRating}</b></span>
                    </div>
                </div>
            </div>
        )
    }
}

export default Movie
