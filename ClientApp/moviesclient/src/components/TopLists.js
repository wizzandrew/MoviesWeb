import React, { Component } from 'react';
import axios from 'axios'
import { Link } from 'react-router-dom'

class TopLists extends Component {
    state = {
        topLists: [],
        newListName: null
    }

    componentDidMount() {
        axios.get('https://movieswebsep6.azurewebsites.net/api/MovieList/1')
            .then(res => {
                //console.log(res);
                this.setState({
                    topLists: res.data
                })
            })
    }

    onNameChange = (e) => {
        this.setState({
            newListName: e.target.value
        })
    }

    onSubmit = (e) => {
        e.preventDefault();
        
        const topList = {
                "_id": this.state.topLists.length + 1,
                "_title": this.state.newListName,
                "_userId": 1,
                "_movieIds": [],
                "_movies": null
              }

        axios.post('https://movieswebsep6.azurewebsites.net/api/MovieList', topList)
            .then(res => {
                console.log(res);
                this.setState({
                    newListName: null
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

        const { topLists } = this.state;
        const lists = topLists.length ? (
            topLists.map(topList => {
                return (
                    <div className="list card" key={topList._id}>
                        <div className="card-content">
                            <Link to={'/toplist/' + topList._id} >
                                <span className="card-title">{topList._title}</span>
                            </Link>
                            <span>  movies count: {topList._movies.length}</span>
                        </div>
                    </div>
                )
            })
        ) : (
            <div className="center">No lists yet</div>
        )

        //real render
        return (
            <div className="container">
                <h4 className="center">Top Lists</h4>
                <div className="inputs">
                        <form onSubmit={this.onSubmit}>
                            <label>
                                List Name:
                                <input style={inputStyle} type="text" onChange={this.onNameChange} />
                            </label>
                            <button>Create List</button>
                        </form>
                    </div>
                {lists}
            </div>
        )
    }
}

export default TopLists