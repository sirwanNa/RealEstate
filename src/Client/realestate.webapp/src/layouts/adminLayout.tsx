import { useState } from "react";
import { Outlet, Link } from "react-router-dom";
import './AdminLayout.css'; // we’ll create CSS separately

const AdminLayout = () => {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [userMenuOpen, setUserMenuOpen] = useState(false);

  const toggleSidebar = () => setSidebarOpen(!sidebarOpen);
  const toggleUserMenu = () => setUserMenuOpen(!userMenuOpen);

  return (
    <div className="admin-container">
      {/* Sidebar */}
      <div className={`sidebar ${sidebarOpen ? 'active' : ''}`}>
        <h2>Admin Panel</h2>
        <Link to="/">🏠 Dashboard</Link>

        <h3>Settings</h3>
        <Link to="/Settings/Builder">⚒ Constructors</Link>
        <Link to="/Settings/Region">📍 Regions</Link>
        <Link to="/Settings/Tag">🏷 Tags</Link>

        <h3>Real Estate</h3>
        <Link to="/Realestate/Houses/List">🏘 Houses</Link>

        <h3>Weblog</h3>
        <Link to="/weblog/posts">📝 Posts</Link>

        <h3>User</h3>
        <Link to="/profile">👤 Edit Profile</Link>
        <Link to="/logout">🚪 Logout</Link>
      </div>

      {/* Main content */}
      <div className="main">
        {/* Topbar */}
        <div className="topbar">
          <button className="menu-toggle" onClick={toggleSidebar}>
            {sidebarOpen ? '✖' : '☰'}
          </button>
          <h1>Dashboard</h1>
          <div className="user-menu">
            <button onClick={toggleUserMenu}>Admin ▾</button>
            {userMenuOpen && (
              <div className="user-dropdown">
                <Link to="/profile">Edit Profile</Link>
                <Link to="/logout">Logout</Link>
              </div>
            )}
          </div>
        </div>

        {/* Page content */}
        <div className="content">
          <Outlet />
        </div>
      </div>
    </div>
  );
};

export default AdminLayout;

