import PropTypes from 'prop-types';
const ListContainer = (props) => {
  const { children, header } = props;

  const renderHeader = () => {
    if (typeof header === 'string') {
      return <h4>{header}</h4>;
    }
    return <>{header}</>;
  };

  return (
    <div className="border m-1 p-1 rounded-md basis-4/12 border-black flex flex-col text-lg">
      {header && <div>{renderHeader()}</div>}
      {children}
    </div>
  );
};

ListContainer.propTypes = {
  header: PropTypes.oneOfType([PropTypes.node, PropTypes.string]),
  children: PropTypes.node,
};

export default ListContainer;
