import React from 'react'
import {Link} from 'react-router-dom'

const Navbar = () => {
    return(
        <nav className="nav-wrapper red darken-3">
            <div className="container">
                <a className="brand-logo">Movies Platform</a>
                <ul className="right">
                    <li><Link to="/">Home</Link></li>
                    {/* <li><Link to="/movie">Movie Search</Link></li> */}
                    <li><Link to="/toplists">Top Lists</Link></li>
                    <li><Link to="/topmovies">Top Movies</Link></li>
                </ul>
            </div>
        </nav>
    )
}

export default Navbar